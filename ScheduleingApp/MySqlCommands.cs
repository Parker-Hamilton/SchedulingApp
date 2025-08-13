using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    public static class MySqlCommands
    {

        public static string CustomerAction(Customer customer, string type)
        {

            string addAddress = $@"
SET @countryId = NULL;
SET @cityId = NULL;
SET @addressId = NULL;
SET @customerId = NULL;

INSERT INTO country (country, createdBy, createDate, lastUpdate, lastUpdateBy)
SELECT '{customer.Country.Trim()}', '{Credentials.Instance.Username}', NOW(), NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM country WHERE country = '{customer.Country.Trim()}'
);

SELECT countryId INTO @countryId
FROM country
WHERE country = '{customer.Country.Trim()}';

INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
SELECT '{customer.City.Trim()}', @countryId, NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM city WHERE city = '{customer.City}' AND countryId = @countryId
);

SELECT cityId INTO @cityId
FROM city
WHERE city = '{customer.City.Trim()}' AND countryId = @countryId;

INSERT INTO address (
    address, address2, cityId, postalCode, phone,
    createDate, createdBy, lastUpdate, lastUpdateBy
)
SELECT '{customer.Address.Trim()}', '{customer.Address2.Trim()}', @cityId, '{customer.PostalCode.Trim()}', '{customer.Phone.Trim()}',
       NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM address
    WHERE address = '{customer.Address.Trim()}'
      AND (address2 = '{customer.Address2.Trim()}' OR (address2 IS NULL AND '{customer.Address2.Trim()}' IS NULL))
      AND postalCode = '{customer.PostalCode.Trim()}'
      AND phone = '{customer.Phone.Trim()}'
      AND cityId = @cityId
);

SELECT addressId INTO @addressId
FROM address
WHERE address = '{customer.Address.Trim()}'
  AND (address2 = '{customer.Address2.Trim()}' OR (address2 IS NULL AND '{customer.Address2.Trim()}' IS NULL))
  AND postalCode = '{customer.PostalCode.Trim()}'
  AND phone = '{customer.Phone.Trim()}'
  AND cityId = @cityId;";

            string addCustomer = $@"
INSERT INTO customer (
    customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy
)
SELECT '{customer.Name.Trim()}', @addressId, 1, NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM customer WHERE customerName = '{customer.Name.Trim()}' AND addressId = @addressId
);";

            string modifyCustomer = $@"
SELECT customerId INTO @customerId
FROM customer
ORDER BY customerId DESC
LIMIT 1;

UPDATE customer
    SET
        customerName = '{customer.Name.Trim()}',
        addressId = @addressId,
        lastUpdate = NOW(),
        lastUpdateBy = '{Credentials.Instance.Username}'
    WHERE customerId = @customerId;
";
            if (type == "add")
            {
                return $"{addAddress} {addCustomer}";
            }
            else if (type == "modify")
            {
                return $"{addAddress} {modifyCustomer}";
            }
            else 
            {
                return "";
            }
        }

        public static string GetAddedCustomerID => @"
SELECT customerId
FROM customer
ORDER BY customerId DESC
LIMIT 1;";

        public static string GetAddedAppointmentID => @"
SELECT appointmentId
FROM appointment
ORDER BY appointmentId DESC
LIMIT 1;";

        public static string AddAppointment(Appointment appointment) { 
            return
            $@"INSERT INTO appointment (
                customerId, userId, title, description, location, contact, type, url,
                start, end, createDate, createdBy, lastUpdate, lastUpdateBy
            )
            VALUES (
                '{appointment.CustomerID}', '{appointment.UserID}', '{appointment.Title.Trim()}', '{appointment.Description.Trim()}', '{appointment.Location.Trim()}', '{appointment.Contact.Trim()}', '{appointment.Type.Trim()}', '{appointment.URL.Trim()}',
                '{appointment.Start.ToString("yyyy-MM-dd HH:mm:ss")}', '{appointment.End.ToString("yyyy-MM-dd HH:mm:ss")}', NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
            );
        "; }

        public static string DeleteAppointment(Appointment appointment) 
        { return $@"
            DELETE FROM appointment WHERE appointmentId = {appointment.Id};"; 
        }

        public static string GetAllAppointments => @"
            SELECT a.appointmentId, a.title, a.description, a.location, a.contact, a.type, a.url,
                   a.start, a.end, a.userId, a.customerId
            FROM appointment AS a
            JOIN customer AS c ON a.customerId = c.customerId
            JOIN user AS u ON a.userId = u.userId;
        ";

        public static string GetAllCustomers => @"
            SELECT c.customerId, c.customerName, a.phone, a.address, a.address2, a.postalCode,
                   city.city, country.country
            FROM customer AS c
            JOIN address AS a ON c.addressId = a.addressId
            JOIN city ON a.cityId = city.cityId
            JOIN country ON city.countryId = country.countryId
            WHERE c.active = 1;
        ";

        public static string GetAllUsers => @"
            SELECT userId, userName FROM `user`;        
        ";

        public static string DeleteCustomer(Customer customer)
        {
            return
                $@"
                DELETE FROM customer WHERE customerId = {customer.CustomerId};
        ";
        }

        public static string UpdateAppointment(Appointment appointment) { 
            return
            $@"UPDATE appointment
            SET customerId = '{appointment.CustomerID}',
                userId = '{appointment.UserID}',
                title = '{appointment.Title.Trim()}',
                description = '{appointment.Description.Trim()}',
                location = '{appointment.Location.Trim()}',
                contact = '{appointment.Contact.Trim()}',
                type = '{appointment.Type.Trim()}',
                url = '{appointment.URL.Trim()}',
                start = '{appointment.Start.ToString("yyyy-MM-dd HH:mm:ss")}',
                end = '{appointment.End.ToString("yyyy-MM-dd HH:mm:ss")}',
                lastUpdate = NOW(),
                lastUpdateBy = '{Credentials.Instance.Username}'
            WHERE appointmentId = '{appointment.Id}';
        "; }
    }
}
