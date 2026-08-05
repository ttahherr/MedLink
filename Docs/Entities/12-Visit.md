# 12 - Visit

**Document Version:** 1.0  
**Project:** MedLink.app  
**Phase:** Phase 2 — Physical Database Design  
**Entity:** Visit  
**Status:** Approved

---

# 1. Entity Information

| Property | Value |
|----------|-------|
| Table Name | Visit |
| Schema | dbo |
| Primary Key | Id |
| Entity Type | Transactional |
| Aggregate | Visit |
| Soft Delete | Supported |

---

# 2. Table Definition

```sql
Visit
```

Represents the clinical encounter resulting from an appointment.

The Visit aggregate is responsible for all clinical workflow, including diagnosis, clinical notes, and prescription generation.

A Visit cannot exist without an Appointment.

---

# 3. Column Specification

| Column | SQL Type | Required | Nullable | Default | Notes |
|---------|----------|----------|----------|----------|------|
| Id | INT IDENTITY | Yes | No | Identity | Primary Key |
| AppointmentId | INT | Yes | No | — | FK → Appointment |
| Diagnosis | NVARCHAR(1000) | No | Yes | NULL | Doctor diagnosis |
| ClinicalNotes | NVARCHAR(MAX) | No | Yes | NULL | Examination notes |
| CreatedAt | DATETIME2(0) | Yes | No | System Time | BaseEntity |
| UpdatedAt | DATETIME2(0) | No | Yes | NULL | BaseEntity |
| IsDeleted | BIT | Yes | No | 0 | Soft Delete |

---

# 4. Primary Key

```text
PK_Visit
(
    Id
)
```

Clustered Primary Key.

---

# 5. Foreign Keys

## FK → Appointment

```text
AppointmentId
→ Appointment(Id)
```

Delete Behavior

```text
Restrict (No Action)
```

---

# 6. Unique Constraints

## UQ_Visit_Appointment

```text
(
    AppointmentId
)
```

Purpose

Guarantees that an appointment can have at most one visit.

---

# 7. Check Constraints

No check constraints are required for this entity.

---

# 8. Indexes

## Clustered Index

```text
PK_Visit
```

---

## Unique Non-Clustered Index

### UQ_Visit_Appointment

```text
AppointmentId
```

Purpose

Enforce the one-to-one relationship between Appointment and Visit.

---

# 9. Navigation Properties

## References

```text
Appointment
```

---

## Child Navigation

```text
Prescription
```

Relationship

```text
One Visit
↓

Zero or One Prescription
```

---

# 10. Business Rules

- Every Visit belongs to exactly one Appointment.
- An Appointment may or may not have a Visit.
- A Visit cannot exist without an Appointment.
- Clinical information is stored in Visit, not Appointment.
- A Visit may produce one Prescription.
- A Visit may exist without a Prescription.
- Soft-deleted visits remain in the database for audit purposes.

---

# 11. Data Integrity Rules

- Appointment must exist before creating a Visit.
- AppointmentId is mandatory.
- Only one Visit may reference the same Appointment.
- Foreign keys cannot be NULL.
- Hard deletes are prevented by database constraints.

---

# 12. EF Core Notes

- Configure using Fluent API only.
- Implement one-to-one relationship using a foreign key plus a UNIQUE constraint.
- Global Query Filter will exclude soft-deleted rows.
- Do not use a shared primary key.

---

# 13. SQL Server Notes

- Use `DATETIME2(0)` for audit timestamps.
- Use `NVARCHAR(1000)` for Diagnosis.
- Use `NVARCHAR(MAX)` for ClinicalNotes.
- Use a clustered primary key on Id.
- Use a UNIQUE index on AppointmentId to enforce one-to-one cardinality.

---

# 14. Physical Design Decisions

| Decision | Reason |
|----------|--------|
| Independent Primary Key | Standard project-wide PK strategy |
| FK + UNIQUE Constraint | Implements one-to-one relationship |
| Restrict Delete Behavior | Preserve medical history |
| Soft Delete | Audit and historical tracking |
| Diagnosis nullable | Some visits may not record a diagnosis immediately |
| ClinicalNotes stored as NVARCHAR(MAX) | Supports detailed clinical documentation |

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