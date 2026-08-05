
# Physical Database Specification v1.0

# Entity 02 — Branch

**Status:** Approved  
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|------|-------|
| Entity Name | Branch |
| Table Name | Branch |
| Domain | Administrative |
| Aggregate Root | No |
| Parent Aggregate | Clinic |
| Description | Represents a physical clinic branch where doctors provide services. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Max Length | Precision | Default | PK | FK | Unique | Indexed | Notes |
|---|---|---|---|---:|---|---|:--:|:--:|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | Yes | - | - | Identity | ✔ | | | Clustered PK | Primary Key |
| ClinicId | INT | int | Yes | - | - | - | | ✔ | | ✔ | FK to Clinic |
| Name | NVARCHAR(200) | string | Yes | 200 | - | - | | | Composite | Via UQ | Branch name |
| Email | NVARCHAR(254) | string | Yes | 254 | - | - | | | ✔ | Via UQ | Global unique |
| PhoneNumber | NVARCHAR(20) | string | Yes | 20 | - | - | | | | | E.164 compatible |
| Country | NVARCHAR(100) | string | Yes |100|-|-||||| Address |
| City | NVARCHAR(100) | string | Yes |100|-|-||||| Address |
| Street | NVARCHAR(200) | string | Yes |200|-|-||||| Address |
| CreatedAt | DATETIME2(3) | DateTime | Yes |-|(3)|SYSUTCDATETIME()||||| Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | No |-|(3)|NULL||||| Audit |
| IsDeleted | BIT | bool | Yes |-|-|0||||| Soft Delete |

---

# 3. Primary Key

- **PK_Branch(Id)**

**Justification**

- Uses the project standard `INT IDENTITY`.
- Clustered primary key.

---

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|------------|-----------|-----------------|
| FK_Branch_Clinic | Clinic(Id) | Restrict / No Action |

**Justification**

A Clinic cannot be physically deleted while Branches still exist.

---

# 5. Unique Constraints

| Constraint | Columns |
|------------|---------|
| UQ_Branch_Email | Email |
| UQ_Branch_Clinic_Name | ClinicId, Name |

**Justification**

- Email is globally unique.
- Branch names must be unique inside the same Clinic.
- Email remains reserved even after Soft Delete.

---

# 6. Check Constraints

None.

Business validation is handled by the application layer.

---

# 7. Indexes

Automatically created:

- PK_Branch
- UQ_Branch_Email
- UQ_Branch_Clinic_Name
- Non-unique index on ClinicId (recommended to optimize joins)

No standalone index on `IsDeleted`.

---

# 8. Navigation Properties

- Clinic
- BranchWorkingHours
- BranchDoctors
- Appointments

---

# 9. Business Rules

- Every Branch belongs to exactly one Clinic.
- Every Branch has its own email.
- Email is globally unique.
- Branch name is unique per Clinic.
- Address is stored as an owned Value Object.
- Soft Delete does not release unique values.

---

# 10. EF Core Notes

- Address mapped as Owned Type.
- Fluent API deferred until Phase 3.
- Global Query Filter will be applied to IsDeleted.

---

# 11. SQL Server Notes

- NVARCHAR for Unicode.
- DATETIME2(3) for timestamps.
- BIT for soft delete.
- INT IDENTITY for primary key.

---

# 12. Physical Design Decisions

1. No standalone index on IsDeleted.
2. Email is globally unique.
3. No explicit index for Email because UNIQUE creates one.
4. Restrict delete behavior from Clinic to Branch.
5. Composite uniqueness on (ClinicId, Name).

---

# 13. Approval

**Status:** Approved for Physical Database Specification v1.0
