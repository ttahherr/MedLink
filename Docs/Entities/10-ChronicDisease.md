# 10 - ChronicDisease

**Document Version:** 1.0  
**Project:** MedLink.app  
**Phase:** Phase 2 — Physical Database Design  
**Entity:** ChronicDisease  
**Status:** Approved

---

# 1. Entity Information

| Property | Value |
|----------|-------|
| Table Name | ChronicDisease |
| Schema | dbo |
| Primary Key | Id |
| Entity Type | Clinical |
| Aggregate | MedicalRecord |
| Soft Delete | Supported |

---

# 2. Table Definition

```sql
ChronicDisease
```

Represents a chronic disease associated with a patient's medical record.

Each record identifies one chronic disease diagnosed for the patient. A medical record may contain multiple chronic diseases.

---

# 3. Column Specification

| Column | SQL Type | Required | Nullable | Default | Notes |
|---------|----------|----------|----------|----------|------|
| Id | INT IDENTITY | Yes | No | Identity | Primary Key |
| MedicalRecordId | INT | Yes | No | — | FK → MedicalRecord |
| Name | NVARCHAR(200) | Yes | No | — | Chronic disease name |
| Notes | NVARCHAR(1000) | No | Yes | NULL | Additional clinical notes |
| CreatedAt | DATETIME2(0) | Yes | No | System Time | BaseEntity |
| UpdatedAt | DATETIME2(0) | No | Yes | NULL | BaseEntity |
| IsDeleted | BIT | Yes | No | 0 | Soft Delete |

---

# 4. Primary Key

```text
PK_ChronicDisease
(
    Id
)
```

Clustered Primary Key.

---

# 5. Foreign Keys

## FK → MedicalRecord

```text
MedicalRecordId
→ MedicalRecord(Id)
```

Delete Behavior

```text
Restrict (No Action)
```

---

# 6. Unique Constraints

No unique constraints are required.

Multiple patients may have the same chronic disease, and a patient may have multiple chronic diseases.

---

# 7. Check Constraints

No check constraints are required.

---

# 8. Indexes

## Clustered Index

```text
PK_ChronicDisease
```

---

## Non-Clustered Index

### IX_ChronicDisease_MedicalRecord

```text
MedicalRecordId
```

Purpose

Efficient retrieval of all chronic diseases belonging to a patient's medical record.

---

# 9. Navigation Properties

## References

```text
MedicalRecord
```

---

## Child Navigation

None.

---

# 10. Business Rules

- Every ChronicDisease belongs to exactly one MedicalRecord.
- A MedicalRecord may contain zero, one, or many ChronicDisease records.
- A ChronicDisease cannot exist without a MedicalRecord.
- Multiple ChronicDisease records may exist for the same MedicalRecord.
- Soft-deleted records remain available for audit and historical purposes.

---

# 11. Data Integrity Rules

- MedicalRecord must exist before creating a ChronicDisease.
- MedicalRecordId is mandatory.
- Name is required.
- Foreign keys cannot be NULL.
- Hard deletes are prevented by database constraints.

---

# 12. EF Core Notes

- Configure using Fluent API only.
- Configure the one-to-many relationship from MedicalRecord to ChronicDisease.
- Global Query Filter will exclude soft-deleted rows.
- Configure string length restrictions using Fluent API.

---

# 13. SQL Server Notes

- Use `NVARCHAR(200)` for disease names.
- Use `NVARCHAR(1000)` for clinical notes.
- Use `DATETIME2(0)` for audit timestamps.
- Use a clustered primary key on Id.
- Create a non-clustered index on `MedicalRecordId` to optimize retrieval.

---

# 14. Physical Design Decisions

| Decision | Reason |
|----------|--------|
| Independent Primary Key | Consistent project-wide PK strategy |
| Restrict Delete Behavior | Preserve medical history |
| One-to-Many relationship | A medical record can contain multiple chronic diseases |
| Soft Delete | Supports auditing and historical tracking |
| Separate entity for chronic diseases | Keeps the medical record normalized and extensible |

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