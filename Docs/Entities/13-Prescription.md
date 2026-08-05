# 13 - Prescription

**Document Version:** 1.0  
**Project:** MedLink.app  
**Phase:** Phase 2 — Physical Database Design  
**Entity:** Prescription  
**Status:** Approved

---

# 1. Entity Information

| Property | Value |
|----------|-------|
| Table Name | Prescription |
| Schema | dbo |
| Primary Key | Id |
| Entity Type | Transactional |
| Aggregate | Prescription |
| Soft Delete | Supported |

---

# 2. Table Definition

```sql
Prescription
```

Represents the medical prescription issued during a patient visit.

A Prescription belongs to exactly one Visit and serves as the parent entity for one or more Prescription Items.

The Prescription entity stores general prescription information, while individual medications are stored in the PrescriptionItem table.

---

# 3. Column Specification

| Column | SQL Type | Required | Nullable | Default | Notes |
|---------|----------|----------|----------|----------|------|
| Id | INT IDENTITY | Yes | No | Identity | Primary Key |
| VisitId | INT | Yes | No | — | FK → Visit |
| Notes | NVARCHAR(1000) | No | Yes | NULL | General prescription notes |
| CreatedAt | DATETIME2(0) | Yes | No | System Time | BaseEntity |
| UpdatedAt | DATETIME2(0) | No | Yes | NULL | BaseEntity |
| IsDeleted | BIT | Yes | No | 0 | Soft Delete |

---

# 4. Primary Key

```text
PK_Prescription
(
    Id
)
```

Clustered Primary Key.

---

# 5. Foreign Keys

## FK → Visit

```text
VisitId
→ Visit(Id)
```

Delete Behavior

```text
Restrict (No Action)
```

---

# 6. Unique Constraints

## UQ_Prescription_Visit

```text
(
    VisitId
)
```

Purpose

Ensures that each Visit can have at most one Prescription.

---

# 7. Check Constraints

No check constraints are required for this entity.

---

# 8. Indexes

## Clustered Index

```text
PK_Prescription
```

---

## Unique Non-Clustered Index

### UQ_Prescription_Visit

```text
VisitId
```

Purpose

Enforces the one-to-one relationship between Visit and Prescription.

---

# 9. Navigation Properties

## References

```text
Visit
```

---

## Child Navigation

```text
PrescriptionItems
```

Relationship

```text
One Prescription
↓

One or Many Prescription Items
```

---

# 10. Business Rules

- Every Prescription belongs to exactly one Visit.
- A Visit may or may not generate a Prescription.
- A Prescription cannot exist without a Visit.
- A Visit can have at most one Prescription.
- A Prescription must contain at least one Prescription Item.
- General notes apply to the entire Prescription.
- Medication details are stored in PrescriptionItem.
- Soft-deleted Prescriptions remain available for audit purposes.

---

# 11. Data Integrity Rules

- Visit must exist before creating a Prescription.
- VisitId is mandatory.
- Only one Prescription may reference the same Visit.
- A Prescription must not remain without at least one Prescription Item after transaction completion.
- Foreign keys cannot be NULL.
- Hard deletes are prevented by database constraints.

---

# 12. EF Core Notes

- Configure using Fluent API only.
- Implement one-to-one relationship using a foreign key plus a UNIQUE constraint.
- Configure one-to-many relationship with PrescriptionItem.
- Global Query Filter will exclude soft-deleted rows.
- Do not use shared primary keys.

---

# 13. SQL Server Notes

- Use `DATETIME2(0)` for audit timestamps.
- Use `NVARCHAR(1000)` for general notes.
- Use clustered primary key on Id.
- Use UNIQUE index on VisitId.
- Use separate non-clustered index on VisitId only if required beyond the unique index.

---

# 14. Physical Design Decisions

| Decision | Reason |
|----------|--------|
| Independent Primary Key | Consistent project-wide strategy |
| FK + UNIQUE Constraint | Implements one-to-one relationship |
| Restrict Delete Behavior | Preserve medical history |
| Soft Delete | Supports audit and historical records |
| Notes stored separately | Keeps medication items normalized |
| One-to-Many with PrescriptionItem | Supports multiple prescribed medications |

---

# 15. Approval Status

| Item | Status |
|------|--------|
| Domain Model | Approved |
| Logical Database Design | Approved |
| Physical Design | Approved |
| Ready for EF Core Mapping | Yes |

---

**Document Status:** Approved

**Database Version:** v1.0