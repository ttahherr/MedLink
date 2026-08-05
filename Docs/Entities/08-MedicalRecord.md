
# Physical Database Specification v1.0

# Entity 08 — MedicalRecord

**Status:** Approved
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | MedicalRecord |
| Table Name | MedicalRecord |
| Domain | Clinical |
| Aggregate Root | Yes |
| Description | Represents the lifetime medical record of a patient. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Default | PK | FK | Unique | Notes |
|---|---|---|:--:|---|:--:|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | Identity | ✔ | | | Primary Key |
| PatientId | INT | int | ✔ | - | | ✔ | ✔ | One-to-One with Patient |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | SYSUTCDATETIME() | | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | NULL | | | | Audit |
| IsDeleted | BIT | bool | ✔ | 0 | | | | Soft Delete |

---

# 3. Primary Key

- PK_MedicalRecord(Id)

---

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|---|---|---|
| FK_MedicalRecord_Patient | Patient(Id) | Restrict |

---

# 5. Unique Constraints

| Constraint | Columns |
|---|---|
| UQ_MedicalRecord_PatientId | PatientId |

Guarantees one Medical Record per Patient.

---

# 6. Check Constraints

None.

---

# 7. Indexes

- PK_MedicalRecord
- Unique index from UQ_MedicalRecord_PatientId
- No standalone index on IsDeleted

---

# 8. Navigation Properties

- Patient
- Allergies
- ChronicDiseases
- Visits

---

# 9. Business Rules

- Every Patient owns exactly one MedicalRecord.
- A MedicalRecord belongs to exactly one Patient.
- MedicalRecord is the Aggregate Root of the Clinical domain.
- Soft Delete does not release unique values.

---

# 10. Data Integrity Rules

- Referenced Patient must exist.
- Duplicate Medical Records for the same Patient are prohibited.

---

# 11. EF Core Notes

- One-to-One with Patient.
- One-to-Many with Allergy.
- One-to-Many with ChronicDisease.
- One-to-Many with Visit.
- Fluent API deferred.

---

# 12. SQL Server Notes

- INT IDENTITY primary key.
- DATETIME2(3) for audit fields.
- BIT for soft delete.

---

# 13. Physical Design Decisions

1. Independent surrogate key.
2. Unique PatientId to enforce one-to-one.
3. Restrict delete behavior.
4. No standalone index on IsDeleted.

---

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
