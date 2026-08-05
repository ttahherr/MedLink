
# Physical Database Specification v1.0

# Entity 03 — BranchWorkingHour

**Status:** Approved  
**Version:** 1.0

---

# 1. Entity Information

| Item | Value |
|---|---|
| Entity Name | BranchWorkingHour |
| Table Name | BranchWorkingHour |
| Domain | Administrative |
| Aggregate Root | No |
| Parent Aggregate | Branch |
| Description | Defines the weekly working schedule for a clinic branch. |

---

# 2. Columns

| Property | SQL Type | CLR Type | Required | Precision / Length | Default | PK | FK | Notes |
|---|---|---|:--:|---|---|:--:|:--:|---|
| Id | INT IDENTITY(1,1) | int | ✔ | - | Identity | ✔ | | Primary Key |
| BranchId | INT | int | ✔ | - | - | | ✔ | FK → Branch |
| DayOfWeek | TINYINT | byte | ✔ | 0-6 | - | | | Enum value |
| OpeningTime | TIME(0) | TimeOnly | ✔ | 0 | - | | | Branch opens |
| ClosingTime | TIME(0) | TimeOnly | ✔ | 0 | - | | | Branch closes |
| IsClosed | BIT | bool | ✔ | - | 0 | | | Closed all day |
| CreatedAt | DATETIME2(3) | DateTime | ✔ | (3) | SYSUTCDATETIME() | | | Audit |
| UpdatedAt | DATETIME2(3) | DateTime? | ✖ | (3) | NULL | | | Audit |
| IsDeleted | BIT | bool | ✔ | - | 0 | | | Soft Delete |

---

# 3. Primary Key

- **PK_BranchWorkingHour(Id)**

---

# 4. Foreign Keys

| Constraint | Reference | Delete Behavior |
|---|---|---|
| FK_BranchWorkingHour_Branch | Branch(Id) | Cascade |

**Justification**

A working-hour record has no meaning without its parent Branch.

---

# 5. Unique Constraints

| Constraint | Columns |
|---|---|
| UQ_BranchWorkingHour_Branch_Day | BranchId, DayOfWeek |

Only one schedule is allowed per day for each branch.

---

# 6. Check Constraints

| Constraint | Rule |
|---|---|
| CK_BranchWorkingHour_DayOfWeek | DayOfWeek BETWEEN 0 AND 6 |
| CK_BranchWorkingHour_TimeRange | IsClosed = 1 OR ClosingTime > OpeningTime |

---

# 7. Indexes

- Clustered PK on Id.
- Unique Index created by `UQ_BranchWorkingHour_Branch_Day`.
- Non-unique index on `BranchId` is unnecessary because the unique composite index starts with BranchId.
- No standalone index on `IsDeleted`.

---

# 8. Navigation Properties

- Branch

---

# 9. Business Rules

- Every record belongs to exactly one Branch.
- A Branch can have at most one record for each day of the week.
- If `IsClosed = true`, opening and closing times are ignored by business logic.
- Soft Delete does not affect uniqueness.

---

# 10. Data Integrity Rules

- DayOfWeek must be valid.
- Opening time must be earlier than closing time unless the branch is marked closed.
- Parent Branch must exist.

---

# 11. EF Core Notes

- Map `DayOfWeek` enum to `TINYINT`.
- Map `TimeOnly` to SQL `TIME(0)`.
- Fluent API will be implemented after approval of all specifications.

---

# 12. SQL Server Notes

- TIME(0) provides minute precision and saves storage.
- DATETIME2(3) used consistently for audit fields.
- INT IDENTITY used for the primary key.

---

# 13. Physical Design Decisions

1. Independent surrogate primary key.
2. Composite unique constraint `(BranchId, DayOfWeek)`.
3. Cascade delete from Branch.
4. No standalone index on IsDeleted.
5. TIME(0) selected because seconds are not required for clinic schedules.

---

# 14. Approval

**Status:** Approved for Physical Database Specification v1.0
