# 14 - PrescriptionItem

**Document Version:** 1.0  
**Project:** MedLink.app  
**Phase:** Phase 2 — Physical Database Design  
**Entity:** PrescriptionItem  
**Status:** Approved

---

# 1. Entity Information

| Property | Value |
|----------|-------|
| Table Name | PrescriptionItem |
| Schema | dbo |
| Primary Key | Id |
| Entity Type | Transactional |
| Aggregate | Prescription |
| Soft Delete | Supported |

---

# 2. Table Definition

```sql
PrescriptionItem
```

Represents a single medication prescribed within a Prescription.

Each PrescriptionItem stores the details of one medication, including dosage, frequency, duration, and administration instructions.

A Prescription must contain one or more Prescription Items.

---

# 3. Column Specification

| Column | SQL Type | Required | Nullable | Default | Notes |
|---------|----------|----------|----------|----------|------|
| Id | INT IDENTITY | Yes | No | Identity | Primary Key |
| PrescriptionId | INT | Yes | No | — | FK → Prescription |
| MedicationName | NVARCHAR(200) | Yes | No | — | Medication name |
| Dosage | NVARCHAR(100) | Yes | No | — | Dose amount or strength |
| Frequency | NVARCHAR(100) | Yes | No | — | Administration frequency |
| Duration | NVARCHAR(100) | Yes | No | — | Treatment duration |
| Instructions | NVARCHAR(1000) | No | Yes | NULL | Additional usage instructions |
| CreatedAt | DATETIME2(0) | Yes | No | System Time | BaseEntity |
| UpdatedAt | DATETIME2(0) | No | Yes | NULL | BaseEntity |
| IsDeleted | BIT | Yes | No | 0 | Soft Delete |

---

# 4. Primary Key

```text
PK_PrescriptionItem
(
    Id
)
```

Clustered Primary Key.

---

# 5. Foreign Keys

## FK → Prescription

```text
PrescriptionId
→ Prescription(Id)
```

Delete Behavior

```text
Restrict (No Action)
```

---

# 6. Unique Constraints

No unique constraints are required.

Multiple medications with the same name may exist within different prescriptions.

---

# 7. Check Constraints

No check constraints are required.

Business validation (such as ensuring non-empty values) is handled at the application level.

---

# 8. Indexes

## Clustered Index

```text
PK_PrescriptionItem
```

---

## Non-Clustered Index

### IX_PrescriptionItem_Prescription

```text
PrescriptionId
```

Purpose

Efficient retrieval of all medications belonging to a prescription.

---

# 9. Navigation Properties

## References

```text
Prescription
```

---

## Child Navigation

None.

---

# 10. Business Rules

- Every PrescriptionItem belongs to exactly one Prescription.
- A Prescription must contain one or more Prescription Items.
- Each PrescriptionItem represents a single medication.
- Medication details are immutable parts of the patient's medical history once finalized.
- Multiple Prescription Items may belong to the same Prescription.
- Soft-deleted Prescription Items remain available for audit purposes.

---

# 11. Data Integrity Rules

- Prescription must exist before creating a PrescriptionItem.
- PrescriptionId is mandatory.
- MedicationName is required.
- Dosage is required.
- Frequency is required.
- Duration is required.
- Foreign keys cannot be NULL.
- Hard deletes are prevented by database constraints.

---

# 12. EF Core Notes

- Configure using Fluent API only.
- Configure one-to-many relationship from Prescription to PrescriptionItem.
- Global Query Filter will exclude soft-deleted rows.
- Required string lengths should be configured using Fluent API.

---

# 13. SQL Server Notes

- Use `NVARCHAR` for all textual medical information to support Unicode.
- Use `DATETIME2(0)` for audit timestamps.
- Use a clustered primary key on Id.
- Create a non-clustered index on `PrescriptionId` to optimize retrieval of prescription items.

---

# 14. Physical Design Decisions

| Decision | Reason |
|----------|--------|
| Independent Primary Key | Consistent project-wide PK strategy |
| Restrict Delete Behavior | Preserve historical prescription data |
| One-to-Many relationship | A prescription can contain multiple medications |
| NVARCHAR for medical text | Supports multilingual medication names and instructions |
| Soft Delete | Supports auditing and historical tracking |
| Separate entity for medication items | Maintains normalization and extensibility |

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