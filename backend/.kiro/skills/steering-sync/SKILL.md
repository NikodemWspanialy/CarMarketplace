---
name: steering-sync
description: Keeps workspace steering files in sync with codebase changes. Use after every code change, refactoring, or when new patterns, conventions, or architectural decisions are introduced. Automatically detects when steering files need updates based on what was changed in the code.
---

# Steering Sync

## Purpose

After each code change, review whether the workspace steering files (`.kiro/steering/*.md`) need to be updated to reflect new patterns, conventions, or architectural decisions introduced in the code.

## When to Run

- After adding new files, classes, or patterns to the codebase
- After refactoring that changes conventions or structure
- After introducing new architectural decisions
- After renaming or reorganizing code

## Instructions

### Step 1: Identify what changed

Look at the code changes made in the current prompt. Determine:
- Were new patterns introduced? (e.g. new factory, guard, helper)
- Were naming conventions changed?
- Were new layers, folders, or file structures added?
- Were new endpoints or API conventions established?
- Were new domain rules or validation patterns added?

### Step 2: Check relevant steering files

The workspace has these steering files:
- `architecture.md` — layer structure, folder organization, shared code locations
- `coding-standards.md` — C# conventions, access modifiers, naming styles
- `naming-conventions.md` — patterns for commands, queries, handlers, DTOs, exceptions
- `ddd-patterns.md` — domain patterns, CQRS, aggregate rules, invariants
- `api-conventions.md` — controller patterns, endpoints, auth, error handling
- `product.md` — product context and capabilities
- `tech.md` — technology stack and dependencies

### Step 3: Decide what needs updating

Only update steering files when:
- A new convention was established that should be followed going forward
- An existing convention was changed
- New endpoints were added (update endpoint list in api-conventions)
- New architectural elements were added (update architecture)

Do NOT update steering files when:
- Changes are implementation details that don't affect conventions
- The change already follows existing steering rules
- It's a bugfix that doesn't introduce new patterns

### Step 4: Apply minimal updates

- Add only what's necessary — don't over-document implementation details
- Keep the same style and tone as the existing steering content
- Don't add specific class names unless they represent a reusable pattern
- Prefer general rules over specific examples
- If unsure whether something belongs in steering — skip it

## Examples

### Should update steering
- Added `[Authorize]` on write endpoints, GET public → update api-conventions Authentication section
- Changed exception naming to drop "Exception" suffix → update naming-conventions and ddd-patterns
- Added new endpoint `PUT /api/car/update-price/{id}` → update api-conventions Endpoints list

### Should NOT update steering
- Fixed a bug in UpdateCarHandler
- Added a new validator for an existing pattern
- Refactored internal handler logic without changing conventions
