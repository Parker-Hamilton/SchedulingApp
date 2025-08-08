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
SELECT '{customer.Country}', '{Credentials.Instance.Username}', NOW(), NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM country WHERE country = '{customer.Country}'
);

SELECT countryId INTO @countryId
FROM country
WHERE country = '{customer.Country}';

INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
SELECT '{customer.City}', @countryId, NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM city WHERE city = '{customer.City}' AND countryId = @countryId
);

SELECT cityId INTO @cityId
FROM city
WHERE city = '{customer.City}' AND countryId = @countryId;

INSERT INTO address (
    address, address2, cityId, postalCode, phone,
    createDate, createdBy, lastUpdate, lastUpdateBy
)
SELECT '{customer.Address}', '{customer.Address2}', @cityId, '{customer.PostalCode}', '{customer.Phone}',
       NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM address
    WHERE address = '{customer.Address}'
      AND (address2 = '{customer.Address2}' OR (address2 IS NULL AND '{customer.Address2}' IS NULL))
      AND postalCode = '{customer.PostalCode}'
      AND phone = '{customer.Phone}'
      AND cityId = @cityId
);

SELECT addressId INTO @addressId
FROM address
WHERE address = '{customer.Address}'
  AND (address2 = '{customer.Address2}' OR (address2 IS NULL AND '{customer.Address2}' IS NULL))
  AND postalCode = '{customer.PostalCode}'
  AND phone = '{customer.Phone}'
  AND cityId = @cityId;";

            string addCustomer = $@"
INSERT INTO customer (
    customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy
)
SELECT '{customer.Name}', @addressId, 1, NOW(), '{Credentials.Instance.Username}', NOW(), '{Credentials.Instance.Username}'
FROM DUAL
WHERE NOT EXISTS (
    SELECT 1 FROM customer WHERE customerName = '{customer.Name}' AND addressId = @addressId
);";

            string modifyCustomer = $@"
SELECT customerId INTO @customerId
FROM customer
ORDER BY customerId DESC
LIMIT 1;

UPDATE customer
    SET
        customerName = '{customer.Name}',
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

        public static string AddAppointment => @"
            INSERT INTO appointment (
                customerId, userId, title, description, location, contact, type, url,
                start, end, createDate, createdBy, lastUpdate, lastUpdateBy
            )
            VALUES (
                @customerId, @userId, @title, @description, @location, @contact, @type, @url,
                @start, @end, NOW(), @createdBy, NOW(), @createdBy
            );

            SELECT LAST_INSERT_ID();
        ";

        public static string UpdateCustomer => @"
            UPDATE customer
            SET customerName = @customerName,
                addressId = @addressId,
                lastUpdate = NOW(),
                lastUpdateBy = @updatedBy
            WHERE customerId = @customerId;
        ";

        public static string DeleteAppointment => @"
            DELETE FROM appointment WHERE appointmentId = @appointmentId;
        ";

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

        public static string UpdateAppointment => @"
            UPDATE appointment
            SET customerId = @customerId,
                userId = @userId,
                title = @title,
                description = @description,
                location = @location,
                contact = @contact,
                type = @type,
                url = @url,
                start = @start,
                end = @end,
                lastUpdate = NOW(),
                lastUpdateBy = @lastUpdateBy
            WHERE appointmentId = @appointmentId;
        ";
    }
}
