USE [Schedules]
GO
/****** Object:  StoredProcedure [dbo].[AddAddress]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE     PROCEDURE [dbo].[AddAddress]
    @address VARCHAR(50),
    @address2 VARCHAR(50),
    @postalCode VARCHAR(10),
    @city VARCHAR(50),
    @country VARCHAR(50),
    @phone VARCHAR(20),
    @createdBy VARCHAR(40)
AS
BEGIN
    DECLARE @cityId INT;
    DECLARE @countryId INT;
    DECLARE @addressId INT;

    BEGIN TRY
        -- Check if country exists
        SELECT @countryId = countryId FROM country WHERE country = @country;
        SELECT @cityId = cityId FROM city WHERE city = @city AND countryId = @countryId;

        IF @countryId IS NULL OR @cityId IS NULL
        BEGIN try
            EXEC AddCity @city, @country, @createdBy;
        END try
        BEGIN CATCH
            PRINT 'Error in Add Address: ' + ERROR_MESSAGE();
        END CATCH
            
       -- Check if address exists for the city
       SELECT @countryId = countryId FROM country WHERE country = @country;
       SELECT @cityId = cityId FROM city WHERE city = @city AND countryId = @countryId;
       SELECT @addressId = addressId FROM address
       WHERE address = @address AND 
       (address2 = @address2 OR (address2 IS NULL AND @address2 IS NULL)) AND 
       postalCode = @postalCode AND 
       phone = @phone AND 
       cityId = @cityId

       IF @addressId IS NULL
       BEGIN
        -- Insert address
        INSERT INTO address (
            address, address2, cityId, postalCode, phone,
            createDate, createdBy, lastUpdate, lastUpdateBy
        )
        VALUES (
            @address, @address2, @cityId, @postalCode, @phone,
            GETDATE(), @createdBy, GETDATE(), @createdBy
        );
            END
    END TRY
    BEGIN CATCH
        PRINT 'Error in Add Address: ' + ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddAppointment]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddAppointment]
    @customerId INT = NULL,
    @userId INT = NULL,
    @title VARCHAR(255) = NULL,
    @description TEXT = NULL,
    @location TEXT = NULL,
    @contact TEXT = NULL,
    @type TEXT = NULL,
    @url VARCHAR(255) = NULL,
    @start DATETIME = NULL,
    @end DATETIME = NULL,
    @createdBy VARCHAR(40) = NULL
AS
BEGIN

    DECLARE @appointmentId INT;
    INSERT INTO dbo.appointment (
        customerId, userId, title, description, location, contact, type, url,
        start, [end], createDate, createdBy, lastUpdate, lastUpdateBy
    )
    VALUES (
        @customerId, @userId, @title, @description, @location, @contact, @type, @url,
        @start, @end, GETDATE(), @createdBy, GETDATE(), @createdBy
    );

    SET @appointmentId = SCOPE_IDENTITY();

    RETURN @appointmentId;

END;
GO
/****** Object:  StoredProcedure [dbo].[AddCity]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[AddCity]
    @city VARCHAR(50),
    @country VARCHAR(50),
    @createdBy VARCHAR(40)
AS
BEGIN
    DECLARE @countryId INT;

    BEGIN TRY
        -- Check if the country exists
        SELECT @countryId = countryId
        FROM country
        WHERE country = @country;

        -- If country doesn't exist, add it and get the new ID
        IF @countryId IS NULL
        BEGIN
            EXEC AddCountry @country, @createdBy;
            SELECT @countryId = countryId FROM country WHERE country = @country;
        END

        -- Now check if the city-country pair exists
        IF NOT EXISTS (
            SELECT 1 FROM city WHERE city = @city AND countryId = @countryId
        )
        BEGIN
            INSERT INTO city (
                city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy
            )
            VALUES (
                @city, @countryId, GETDATE(), @createdBy, GETDATE(), @createdBy
            );
        END
    END TRY
    BEGIN CATCH
        PRINT 'An error occurred: ' + ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddCountry]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCountry]
    @country VARCHAR(50),
    @createdBy VARCHAR(40)
AS
IF NOT EXISTS (
    SELECT * FROM country WHERE country = @country )
BEGIN
    BEGIN TRY
        INSERT INTO country (country, createdBy, createDate, lastUpdate, lastUpdateBy)
        --CURRENT_TIMESTAMP
        VALUES (@country, @createdBy, GETDATE(), GETDATE(), @createdBy);
    END TRY
    BEGIN CATCH
        PRINT 'Error adding country: ' + ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[AddCustomer]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCustomer]
    @customerName VARCHAR(45),
    @address VARCHAR(50),
    @address2 VARCHAR(50),
    @postalCode VARCHAR(10),
    @phone VARCHAR(20),
    @city VARCHAR(50),
    @country VARCHAR(50),
    @createdBy VARCHAR(40)
AS
BEGIN
    DECLARE @countryId INT;
    DECLARE @cityId INT;
    DECLARE @addressId INT;
    DECLARE @customerId INT;

    BEGIN TRY
        -- Call stored procedure to ensure address and related city/country exist
        EXEC AddAddress
            @address = @address,
            @address2 = @address2,
            @postalCode = @postalCode,
            @phone = @phone,
            @city = @city,
            @country = @country,
            @createdBy = @createdBy;

        -- Retrieve the addressId
       SELECT @countryId = countryId FROM country WHERE country = @country;
       SELECT @cityId = cityId FROM city WHERE city = @city AND countryId = @countryId;
       SELECT @addressId = addressId FROM address
       WHERE address = @address AND 
       (address2 = @address2 OR (address2 IS NULL AND @address2 IS NULL)) AND 
       postalCode = @postalCode AND 
       phone = @phone AND 
       cityId = @cityId

        -- Insert the customer record
        IF NOT EXISTS (
            SELECT 1 FROM customer
            WHERE customerName = @customerName AND addressId = @addressId
        )
        BEGIN
            INSERT INTO customer (
                customerName,
                addressId,
                active,
                createDate,
                createdBy,
                lastUpdate,
                lastUpdateBy
            )
            VALUES (
                @customerName,
                @addressId,
                1,
                GETDATE(),
                @createdBy,
                GETDATE(),
                @createdBy
            );
        END
        
SELECT TOP 1 @customerId = customerId FROM customer
ORDER BY customerId DESC;

    END TRY
    BEGIN CATCH
        PRINT 'Error in AddCustomer: ' + ERROR_MESSAGE();
    END CATCH
    RETURN @customerId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAppointment]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteAppointment]
    @appointmentID INT

AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM appointment
    WHERE appointmentId = @appointmentID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllAppointments]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllAppointments]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT a.appointmentId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.[end], a.userId, a.customerId
    FROM appointment AS a
    JOIN customer AS c ON a.customerId = c.customerId
    JOIN [user] AS u ON a.userId = u.userId
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomers]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCustomers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.customerId, c.customerName, a.phone, a.address, a.address2, a.postalCode, city.city, country.country  FROM customer as c
    Join address as a ON c.addressId = a.addressId
    Join city as city ON a.cityId = city.cityId
    Join country as country on city.countryId = country.countryId
    WHERE c.active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT userId, userName FROM [user]
END
GO
/****** Object:  StoredProcedure [dbo].[MarkCustomerInactive]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MarkCustomerInactive]
    @customerID INT

AS
BEGIN
    SET NOCOUNT ON;

    UPDATE customer
    SET
        active = 0
    WHERE customerID = @customerID;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateAppointment]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAppointment]
    @appointmentId INT,
    @customerId INT = NULL,
    @userId INT = NULL,
    @title VARCHAR(255) = NULL,
    @description TEXT = NULL,
    @location TEXT = NULL,
    @contact TEXT = NULL,
    @type TEXT = NULL,
    @url VARCHAR(255) = NULL,
    @start DATETIME = NULL,
    @end DATETIME = NULL,
    @lastUpdateBy VARCHAR(40) = NULL
AS
BEGIN
    UPDATE dbo.appointment
    SET
        customerId = @customerId,
        userId = @userId,
        title = @title,
        description = @description,
        location = @location,
        contact = @contact,
        type = @type,
        url = @url,
        start = @start,
        [end] = @end,
        lastUpdate = GETDATE(),
        lastUpdateBy = @lastUpdateBy
    WHERE appointmentId = @appointmentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 8/7/2025 9:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCustomer]
    @customerID INT,
    @customerName VARCHAR(100),
    @address VARCHAR(255),
    @address2 VARCHAR(255),
    @postalCode VARCHAR(20),
    @phone VARCHAR(20),
    @city VARCHAR(100),
    @country VARCHAR(100),
    @updatedBy VARCHAR(40)

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @cityId INT;
    DECLARE @countryId INT;
    DECLARE @addressId INT;
        -- Call stored procedure to ensure address and related city/country exist
        EXEC AddAddress
            @address = @address,
            @address2 = @address2,
            @postalCode = @postalCode,
            @phone = @phone,
            @city = @city,
            @country = @country,
            @createdBy = @updatedBy;
        -- Retrieve the addressId
       SELECT @countryId = countryId FROM country WHERE country = @country;
       SELECT @cityId = cityId FROM city WHERE city = @city AND countryId = @countryId;
       SELECT @addressId = addressId FROM address
       WHERE address = @address AND 
       (address2 = @address2 OR (address2 IS NULL AND @address2 IS NULL)) AND 
       postalCode = @postalCode AND 
       phone = @phone AND 
       cityId = @cityId

    UPDATE customer
    SET
        customerName = @customerName,
        addressId = @addressId,
        lastUpdate = GETDATE(),
        lastUpdateBy = @updatedBy
    WHERE customerID = @customerID;
END;
GO
