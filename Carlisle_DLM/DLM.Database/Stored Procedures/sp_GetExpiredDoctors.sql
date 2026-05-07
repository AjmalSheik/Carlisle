CREATE PROCEDURE sp_GetExpiredDoctors
AS
BEGIN
    SELECT *
    FROM Doctors
    WHERE LicenseExpiryDate < GETDATE()
      AND IsDeleted = 0
END