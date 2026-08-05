
# Physical Database Specification v1.0

# Entity 07 — Patient

**Status:** Approved
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | Patient |
| Table Name | Patient |
| Domain | Administrative |
| Aggregate Root | Yes (Patient Aggregate) |
| Description | Represents a patient registered in the MedLink system. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Max Length | Default | PK | FK | Unique | Notes |
|---|---|---|:--:|---:|---|:--:|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | - | Identity | ✔ | | | Primary Key |
| ApplicationUserId | NVARCHAR(450) | string | ✔ | 450 | - | | ✔ | ✔ | One-to-One with AspNetUsers |
| DateOfBirth | DATE | DateOnly | ✔ | - | - | | | | Birth date |
| Gender | TINYINT | byte | ✔ | - | - | | | | Enum |
| BloodType | TINYINT | byte | ✖ | - | NULL | | | | Enum |
| EmergencyContactName | NVARCHAR(200) | string | ✖ | 200 | NULL | | | | Optional |
| EmergencyContactPhone | NVARCHAR(20) | string | ✖ | 20 | NULL | | | | E.164 |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | - | SYSUTCDATETIME() | | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | - | NULL | | | | Audit |
| IsDeleted | BIT | bool | ✔ | - | 0 | | | | Soft Delete |

---

# 3. Primary Key

- PK_Patient(Id)

---

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|---|---|---|
| FK_Patient_ApplicationUser | AspNetUsers(Id) | Restrict |

---

# 5. Unique Constraints

| Constraint | Columns |
|---|---|
| UQ_Patient_ApplicationUserId | ApplicationUserId |

---

# 6. Check Constraints

| Constraint | Rule |
|---|---|
| CK_Patient_DateOfBirth | DateOfBirth <= CURRENT_DATE |

---

# 7. Indexes

- PK_Patient
- Unique index from UQ_Patient_ApplicationUserId
- No standalone index on IsDeleted

---

# 8. Navigation Properties

- ApplicationUser
- MedicalRecord
- Appointments

---

# 9. Business Rules

- Every Patient is linked to exactly one Identity account.
- A Patient owns exactly one MedicalRecord.
- A Patient may have many Appointments.
- Patient is not assigned to a specific Branch.
- Soft Delete does not release unique values.

---

# 10. Data Integrity Rules

- Identity user must exist.
- Date of birth cannot be in the future.

---

# 11. EF Core Notes

- One-to-One with ApplicationUser.
- One-to-One with MedicalRecord.
- One-to-Many with Appointment.
- Fluent API deferred.

---

# 12. SQL Server Notes

- DATE used because time component is unnecessary.
- DATETIME2(3) for audit fields.
- NVARCHAR for Unicode text.

---

# 13. Physical Design Decisions

1. Surrogate INT IDENTITY primary key.
2. Unique ApplicationUserId.
3. Restrict delete behavior.
4. No standalone index on IsDeleted.
5. DATE selected for DateOfBirth.

---

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
