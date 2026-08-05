Physical Database Specification v1.0
Entity 05 — Doctor

Status: Approved
Version: 1.0

1. Entity Information
Item	Value
Entity Name	Doctor
Table Name	Doctor
Domain	Administrative
Aggregate Root	No
Parent Aggregate	Clinic
Description	Represents a doctor working for a clinic.
2. Columns
Property	SQL Type	CLR Type	Required	Max Length	Precision	Default	PK	FK	Unique	Notes
Id	INT IDENTITY(1,1)	int	✔	-	-	Identity	✔			Primary Key
ClinicId	INT	int	✔	-	-	-		✔		FK → Clinic
SpecializationId	INT	int	✔	-	-	-		✔		FK → Specialization
ApplicationUserId	NVARCHAR(450)	string	✔	450	-	-		✔	✔	One-to-One with ASP.NET Identity
LicenseNumber	NVARCHAR(50)	string	✔	50	-	-			✔	Medical license number
YearsOfExperience	TINYINT	byte	✔	-	-	0				Years of experience
ConsultationFee	DECIMAL(10,2)	decimal	✔	-	(10,2)	0				Consultation fee
Biography	NVARCHAR(2000)	string?	✖	2000	-	NULL				Optional biography
CreatedAt	DATETIME2(3)	DateTime	✔	-	(3)	SYSUTCDATETIME()				Audit
UpdatedAt	DATETIME2(3)	DateTime?	✖	-	(3)	NULL				Audit
IsDeleted	BIT	bool	✔	-	-	0				Soft Delete
3. Primary Key
PK_Doctor(Id)
4. Foreign Keys
Constraint	Reference	Delete Behavior
FK_Doctor_Clinic	Clinic(Id)	Restrict
FK_Doctor_Specialization	Specialization(Id)	Restrict
FK_Doctor_ApplicationUser	AspNetUsers(Id)	Restrict
Justification
Doctor cannot exist without a Clinic.
Doctor must belong to one approved Specialization.
Doctor profile depends on its Identity user.
5. Unique Constraints
Constraint	Columns
UQ_Doctor_ApplicationUserId	ApplicationUserId
UQ_Doctor_LicenseNumber	LicenseNumber
6. Check Constraints
Constraint	Rule
CK_Doctor_YearsOfExperience	YearsOfExperience >= 0
CK_Doctor_ConsultationFee	ConsultationFee >= 0
7. Indexes

Automatically created:

PK_Doctor
UQ_Doctor_ApplicationUserId
UQ_Doctor_LicenseNumber

Additional indexes:

IX_Doctor_ClinicId
IX_Doctor_SpecializationId

No standalone index on IsDeleted.

8. Navigation Properties
Clinic
Specialization
ApplicationUser
BranchDoctors
9. Business Rules
Every Doctor belongs to exactly one Clinic.
Every Doctor has exactly one Specialization.
Every Doctor is linked to one ASP.NET Identity user.
License Number must be globally unique.
Consultation Fee cannot be negative.
Soft Delete does not release unique values.
10. Data Integrity Rules
Clinic must exist.
Specialization must exist.
Identity User must exist.
License Number is immutable after creation (business rule).
11. EF Core Notes
One-to-One relationship with ApplicationUser.
One-to-Many with Clinic.
One-to-Many with Specialization.
Many-to-Many with Branch through BranchDoctor.
Fluent API will be implemented after approval of all specifications.
12. SQL Server Notes
DECIMAL(10,2) مناسب لتخزين الرسوم المالية.
TINYINT كافٍ لسنوات الخبرة (0–255).
NVARCHAR(450) متوافق مع مفتاح ASP.NET Identity الافتراضي.
DATETIME2(3) لجميع حقول التدقيق.
13. Physical Design Decisions
Independent surrogate primary key.
Restrict delete behavior for all parent entities.
Global unique License Number.
Unique ApplicationUserId لضمان علاقة One-to-One.
No standalone index on IsDeleted.
Decimal precision (10,2) for monetary values.
14. Approval

Status: Approved for Physical Database Specification v1.0