CREATE PROCEDURE sp_GetDoctors
    @Search NVARCHAR(100) = NULL,
    @Status INT = NULL
AS
BEGIN

    SELECT 
        Id,
        FullName,
        Email,
        Specialization,
        LicenseNumber,
        LicenseExpiryDate,

        CASE 
       

            WHEN LicenseExpiryDate < GETDATE() THEN 2
            ELSE Status
        END AS Status,

         CASE 
        WHEN LicenseExpiryDate < GETDATE() THEN 'Expired'
        WHEN Status = 1 THEN 'Active'
        WHEN Status = 2 THEN 'Inactive'
        ELSE 'Unknown'
    END AS StatusName,

        CreatedDate
    FROM Doctors
    WHERE 
        IsDeleted = 0
        AND (
            @Search IS NULL 
            OR FullName LIKE '%' + @Search + '%'
            OR LicenseNumber LIKE '%' + @Search + '%'
        )
        AND (@Status IS NULL OR Status = @Status)

END