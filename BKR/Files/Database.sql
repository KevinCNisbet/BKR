CREATE TABLE tblBKR_Master (
    Contract VARCHAR(16),
    Kredietnemernaam VARCHAR(24),
    Voorletters VARCHAR(5),
    Prefix VARCHAR(8),
    Geboortedatum DATE,
    Straat VARCHAR(24),
    Huisnummer VARCHAR(5),
    Alfanumeriek1 VARCHAR(5),
    Postcode VARCHAR(4),
    Alfanumeriek2 VARCHAR(2),
    Woonplaats VARCHAR(24),
    Contractnummer VARCHAR(16),
    Contractsoort VARCHAR(2),
    Deelnemernummer VARCHAR(7),
    Registratiedatum DATE,
    DatumLaatsteMutatie DATE,
    LimietContractBedrag DECIMAL(8, 0),
    Opnamebedrag DECIMAL(8, 0),
    DatumEersteAflossing DATE,
    DatumTLaatsteAflossing DATE,
    DatumPLaatsteAflossing DATE,
    IndicatieBKRAfgelost VARCHAR(1),
    AchterstCode1 VARCHAR(1),
    DatumAchterstCode1 DATE,
    AchterstCode2 VARCHAR(1),
    DatumAchterstCode2 DATE,
    AchterstCode3 VARCHAR(1),
    DatumAchterstCode3 DATE,
    AchterstCode4 VARCHAR(1),
    DatumAchterstCode4 DATE,
    AchterstCode5 VARCHAR(1),
    DatumAchterstCode5 DATE,
    AchterstCode6 VARCHAR(1),
    DatumAchterstCode6 DATE,
    AchterstCode7 VARCHAR(1),
    DatumAchterstCode7 DATE,
    Geslacht VARCHAR(6),
    LandCode VARCHAR(3)
);

SELECT *
INTO tblBKR_Delta
FROM tblBKR_Master
WHERE 1 = 0;

CREATE TABLE tblCustomer (
    Customer VARCHAR(16),
    Kredietnemernaam VARCHAR(24),
    Voorletters VARCHAR(5),
    Prefix VARCHAR(8),
    Geboortedatum DATE,
    Straat VARCHAR(24),
    Alfanumeriek1 VARCHAR(5),
    Huisnummer VARCHAR(5),
    Postcode VARCHAR(4),
    Alfanumeriek2 VARCHAR(2),
    Woonplaats VARCHAR(24),
    Geslacht VARCHAR(6),
    LandCode VARCHAR(3)
);


CREATE TABLE tblContract (
	Customer1 VARCHAR(16),
	Customer2 VARCHAR(16),
    Contractnummer VARCHAR(16),
    Contractsoort VARCHAR(2),
    Deelnemernummer VARCHAR(7),
    LimietContractBedrag DECIMAL(8, 0),
    Opnamebedrag DECIMAL(8, 0),
    DatumEersteAflossing DATE,
    DatumTLaatsteAflossing DATE,
    DatumPLaatsteAflossing DATE,
    IndicatieBKRAfgelost VARCHAR(1),
	NumberOfPaymentsMissed DECIMAL(4,0)
);

CREATE TABLE tblRegistration (
    TransactionCode NVARCHAR(2),
    Date NVARCHAR(8),
    ParticipantNo NVARCHAR(7),
    Customer NVARCHAR(12),
    Kredietnemernaam NVARCHAR(24),
    Voorletters NVARCHAR(5),
    Voornaam NVARCHAR(12),
    Prefix NVARCHAR(8),
    Geboortedatum NVARCHAR(8),
    Geslacht NVARCHAR(1),
    Straat NVARCHAR(24),
    Huisnummer NVARCHAR(5),
    Alfanumeriek1 NVARCHAR(5),
    Woonplaats NVARCHAR(24),
    Postcode NVARCHAR(4),
    Alfanumeriek2 NVARCHAR(2),
    LandCode NVARCHAR(3),
    GeboortedatumNieuw NVARCHAR(8),
    Contractsoort NVARCHAR(3),
    Contract NVARCHAR(16),
    ContractNieuw NVARCHAR(16),
    LimietContractBedrag NVARCHAR(8),
    Opnamebedrag NVARCHAR(8),
    DatumEersteAflossing NVARCHAR(8),
    DatumTLaatstAflossing NVARCHAR(8),
    DatumPLaatstAflossing NVARCHAR(8),
    SpecialCode NVARCHAR(1),
    RegRegistrDate NVARCHAR(8),
    JointContract NVARCHAR(1),
    NewName NVARCHAR(24),
    CodeRemovalReason NVARCHAR(2),
    BestandCode NVARCHAR(2)
);


select * from tblBKR_Master;
select * from tblBKR_Delta;
select * from tblContract;
select * from tblCustomer;
select * from tblRegistration;