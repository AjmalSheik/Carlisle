CREATE TABLE Doctors (
    Id UNIQUEIDENTIFIER NOT NULL 
        CONSTRAINT PK_Doctors PRIMARY KEY,

    FullName NVARCHAR(200) NOT NULL,

    Email NVARCHAR(200) NOT NULL 
        CONSTRAINT UQ_Doctors_Email UNIQUE,

    Specialization NVARCHAR(100) NOT NULL,

    LicenseNumber NVARCHAR(100) NOT NULL 
        CONSTRAINT UQ_Doctors_LicenseNumber UNIQUE,

    LicenseExpiryDate DATE NOT NULL,

    Status INT NOT NULL 
        CONSTRAINT DF_Doctors_Status DEFAULT 1,

    CreatedDate DATETIME NOT NULL 
        CONSTRAINT DF_Doctors_CreatedDate DEFAULT GETDATE(),

    IsDeleted BIT NOT NULL 
        CONSTRAINT DF_Doctors_IsDeleted DEFAULT 0
);

