
# Physical Database Specification v1.0

# Entity 04 — Specialization

**Status:** Approved  
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | Specialization |
| Table Name | Specialization |
| Domain | Administrative |
| Aggregate Root | No (Global Lookup) |
| Description | Global catalog of medical specializations shared by all clinics. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Max Length | Default | PK | FK | Unique | Notes |
|---|---|---|:--:|---:|---|:--:|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | - | Identity | ✔ | | | Primary Key |
| Name | NVARCHAR(150) | string | ✔ | 150 | - | | | ✔ | Global specialization name |
| Description | NVARCHAR(500) | string? | ✖ | 500 | NULL | | | | Optional description |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | - | SYSUTCDATETIME() | | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | - | NULL | | | | Audit |
| IsDeleted | BIT | bool | ✔ | - | 0 | | | | Soft Delete |

---

# 3. Primary Key

- **PK_Specialization(Id)**

---

# 4. Foreign Keys

None.

---

# 5. Unique Constraints

| Constraint | Columns |
|---|---|
| UQ_Specialization_Name | Name |

The specialization name is globally unique and cannot be reused after Soft Delete.

---

# 6. Check Constraints

None.

Validation rules are enforced by the application layer.

---

# 7. Indexes

- Clustered Primary Key on `Id`.
- Unique index created automatically by `UQ_Specialization_Name`.
- No standalone index on `IsDeleted`.

---

# 8. Navigation Properties

- Doctors

---

# 9. Business Rules

- Represents a global lookup table.
- Shared across all clinics.
- Name must be unique.
- Soft Delete does not release unique values.

---

# 10. Data Integrity Rules

- Name is required.
- Parent records are not applicable.
- Referential integrity is enforced by dependent entities.

---

# 11. EF Core Notes

- Seed data may be used for initial system specializations.
- Fluent API configuration deferred until implementation phase.

---

# 12. SQL Server Notes

- NVARCHAR used for Unicode support.
- DATETIME2(3) used for audit columns.
- INT IDENTITY used for the primary key.

---

# 13. Physical Design Decisions

1. Global lookup table.
2. Independent surrogate primary key.
3. Unique constraint on Name.
4. No standalone index on IsDeleted.
5. Soft Delete retained for consistency with BaseEntity.

---

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
