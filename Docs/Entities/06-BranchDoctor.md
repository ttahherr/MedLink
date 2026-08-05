
# Physical Database Specification v1.0

# Entity 06 — BranchDoctor

**Status:** Approved  
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | BranchDoctor |
| Table Name | BranchDoctor |
| Domain | Administrative |
| Entity Type | Junction Entity |
| Description | Associates Doctors with Branches where they practice. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Default | PK | FK | Unique | Notes |
|---|---|---|:--:|---|:--:|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | Identity | ✔ | | | Surrogate key |
| BranchId | INT | int | ✔ | - | | ✔ | Composite | FK → Branch |
| DoctorId | INT | int | ✔ | - | | ✔ | Composite | FK → Doctor |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | SYSUTCDATETIME() | | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | NULL | | | | Audit |
| IsDeleted | BIT | bool | ✔ | 0 | | | | Soft Delete |

---

# 3. Primary Key

- PK_BranchDoctor(Id)

Independent surrogate primary key as per project standard.

---

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|---|---|---|
| FK_BranchDoctor_Branch | Branch(Id) | Restrict |
| FK_BranchDoctor_Doctor | Doctor(Id) | Restrict |

---

# 5. Unique Constraints

| Constraint | Columns |
|---|---|
| UQ_BranchDoctor_Branch_Doctor | BranchId, DoctorId |

Prevents duplicate doctor assignments within the same branch.

---

# 6. Check Constraints

None.

---

# 7. Indexes

- PK_BranchDoctor
- Unique index from UQ_BranchDoctor_Branch_Doctor
- No standalone index on IsDeleted

---

# 8. Navigation Properties

- Branch
- Doctor
- Appointments

---

# 9. Business Rules

- A Doctor may work in multiple Branches.
- A Branch may have multiple Doctors.
- A Doctor cannot be assigned to the same Branch more than once.
- Assignment history is preserved through Soft Delete policy.

---

# 10. Data Integrity Rules

- Referenced Branch must exist.
- Referenced Doctor must exist.
- Duplicate Branch/Doctor pairs are prohibited.

---

# 11. EF Core Notes

- Explicit junction entity (not implicit many-to-many).
- Fluent API deferred until implementation phase.

---

# 12. SQL Server Notes

- INT IDENTITY primary key.
- DATETIME2(3) audit fields.
- BIT for soft delete.

---

# 13. Physical Design Decisions

1. Surrogate primary key.
2. Composite unique constraint (BranchId, DoctorId).
3. Restrict delete behavior.
4. No standalone index on IsDeleted.
5. Explicit junction entity to support future extensibility.

---

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
