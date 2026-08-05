
# Physical Database Specification v1.0

# Entity 09 — Allergy

**Status:** Approved
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | Allergy |
| Table Name | Allergy |
| Domain | Clinical |
| Parent Aggregate | MedicalRecord |
| Description | Stores a patient's allergy information. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Max Length | Default | PK | FK | Notes |
|---|---|---|:--:|---:|---|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | - | Identity | ✔ | | Primary Key |
| MedicalRecordId | INT | int | ✔ | - | - | | ✔ | FK → MedicalRecord |
| AllergenName | NVARCHAR(200) | string | ✔ | 200 | - | | | Allergy source |
| Reaction | NVARCHAR(500) | string | ✔ | 500 | - | | | Reaction details |
| Severity | TINYINT | byte | ✔ | - | - | | | Enum |
| Notes | NVARCHAR(1000) | string? | ✖ |1000| NULL | | | Optional |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | - | SYSUTCDATETIME() | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | - | NULL | | | Audit |
| IsDeleted | BIT | bool | ✔ | - | 0 | | | Soft Delete |

---

# 3. Primary Key

- PK_Allergy(Id)

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|---|---|---|
| FK_Allergy_MedicalRecord | MedicalRecord(Id) | Cascade |

# 5. Unique Constraints

None.

# 6. Check Constraints

| Constraint | Rule |
|---|---|
| CK_Allergy_Severity | Severity BETWEEN 0 AND 3 |

# 7. Indexes

- PK_Allergy
- IX_Allergy_MedicalRecordId
- No standalone index on IsDeleted

# 8. Navigation Properties

- MedicalRecord

# 9. Business Rules

- Every Allergy belongs to one MedicalRecord.
- A MedicalRecord may contain many Allergy records.
- Severity is required.
- Soft Delete preserves history.

# 10. Data Integrity Rules

- Parent MedicalRecord must exist.

# 11. EF Core Notes

- One-to-Many from MedicalRecord.
- Fluent API deferred.

# 12. SQL Server Notes

- NVARCHAR for multilingual text.
- DATETIME2(3) for audit fields.
- TINYINT stores Severity enum efficiently.

# 13. Physical Design Decisions

1. Surrogate INT IDENTITY key.
2. Cascade delete from MedicalRecord.
3. Index on MedicalRecordId for aggregate queries.
4. No standalone index on IsDeleted.

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
