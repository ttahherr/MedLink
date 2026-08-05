# 11 - Appointment

**Document Version:** 1.0  
**Project:** MedLink.app  
**Phase:** Phase 2 — Physical Database Design  
**Entity:** Appointment  
**Status:** Approved

---

# 1. Entity Information

| Property | Value |
|----------|-------|
| Table Name | Appointment |
| Schema | dbo |
| Primary Key | Id |
| Entity Type | Transactional |
| Aggregate | Appointment |
| Soft Delete | Supported |

---

# 2. Table Definition

```sql
Appointment
```

Represents a scheduled appointment between a patient and a doctor at a specific clinic branch.

This entity is responsible only for the administrative appointment lifecycle.

Clinical data belongs to the Visit aggregate.

---

# 3. Column Specification

| Column | SQL Type | Required | Nullable | Default | Notes |
|---------|----------|----------|----------|----------|------|
| Id | INT IDENTITY | Yes | No | Identity | Primary Key |
| PatientId | INT | Yes | No | — | FK → Patient |
| BranchDoctorId | INT | Yes | No | — | FK → BranchDoctor |
| AppointmentDateTime | DATETIME2(0) | Yes | No | — | Scheduled date & time |
| Status | TINYINT | Yes | No | 1 | Appointment Status |
| Notes | NVARCHAR(500) | No | Yes | NULL | Administrative notes |
| CreatedAt | DATETIME2(0) | Yes | No | System Time | BaseEntity |
| UpdatedAt | DATETIME2(0) | No | Yes | NULL | BaseEntity |
| IsDeleted | BIT | Yes | No | 0 | Soft Delete |

---

# 4. Primary Key

```text
PK_Appointment
(
    Id
)
```

Clustered Primary Key.

---

# 5. Foreign Keys

## FK → Patient

```text
PatientId
→ Patient(Id)
```

Delete Behavior

```
Restrict (No Action)
```

---

## FK → BranchDoctor

```text
BranchDoctorId
→ BranchDoctor(Id)
```

Delete Behavior

```
Restrict (No Action)
```

---

# 6. Unique Constraints

## UQ_Appointment_Doctor_DateTime

```text
(
    BranchDoctorId,
    AppointmentDateTime
)
```

Purpose

Prevent double-booking of the same doctor at the same date and time.

---

# 7. Check Constraints

## CK_Appointment_Status

Allowed values

```text
1 = Scheduled
2 = Completed
3 = Cancelled
4 = NoShow
```

Constraint

```text
Status IN (1,2,3,4)
```

---

# 8. Indexes

## Clustered Index

```
PK_Appointment
```

---

## Non-Clustered Index

### IX_Appointment_Patient

```text
PatientId
```

Purpose

Fast retrieval of patient appointment history.

---

### IX_Appointment_BranchDoctor

```text
BranchDoctorId
```

Purpose

Doctor schedule lookup.

---

### IX_Appointment_DateTime

```text
AppointmentDateTime
```

Purpose

Daily schedule queries.

---

### UQ_Appointment_Doctor_DateTime

Unique Index

```text
BranchDoctorId,
AppointmentDateTime
```

Purpose

Prevent scheduling conflicts.

---

# 9. Navigation Properties

## References

```text
Patient
```

```text
BranchDoctor
```

---

## Child Navigation

```text
Visit
```

Relationship

```
One Appointment
↓

Zero or One Visit
```

---

# 10. Business Rules

- Every appointment belongs to exactly one patient.
- Every appointment belongs to exactly one BranchDoctor assignment.
- A patient may have many appointments.
- A BranchDoctor may have many appointments.
- An appointment may or may not result in a Visit.
- Clinical information is stored in Visit, not Appointment.
- Multiple appointments for the same patient are allowed.
- Two appointments cannot exist for the same doctor at the same date and time.
- Soft-deleted appointments remain in the database.

---

# 11. Data Integrity Rules

- Patient must exist before creating an appointment.
- BranchDoctor must exist before creating an appointment.
- Status must contain a valid enumeration value.
- AppointmentDateTime is mandatory.
- Foreign keys cannot be NULL.
- Hard deletes are protected by database constraints.

---

# 12. EF Core Notes

- Configure using Fluent API only.
- No Data Annotations for database mapping.
- Global Query Filter will exclude soft-deleted rows.
- Appointment Status should be mapped as an Enum.

---

# 13. SQL Server Notes

- Use `DATETIME2(0)` for AppointmentDateTime.
- Use `TINYINT` for Status.
- Use `NVARCHAR(500)` for Notes.
- Use clustered primary key on Id.
- Use unique index to prevent doctor scheduling conflicts.

---

# 14. Physical Design Decisions

| Decision | Reason |
|----------|--------|
| Independent Primary Key | Consistent project-wide PK strategy |
| Unique (BranchDoctorId + AppointmentDateTime) | Prevent doctor double-booking |
| Restrict Delete Behavior | Preserve appointment history |
| Soft Delete | Business audit requirements |
| Status stored as TINYINT | Efficient enum storage |
| DATETIME2(0) | Second-level precision is sufficient |

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