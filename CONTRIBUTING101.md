# Contributing Guidelines

These rules exist to keep collaboration clean, predictable, and low-friction. Follow them strictly unless the team lead agrees otherwise.

### Change Types

* `feat` – new functionality
* `fix` – bug fixes
* `refactor` – internal restructuring
* `docs` – documentation only
* `chore` – tooling, configuration, cleanup
* `build` – CI/CD pipeline, dependency updates
* `test` – unit tests, component tests, coverage
* `tech-debt` – system maintenance, reworks
* `frontend` – UI, interface logic
* `backend` – business logic
* `database` – database, CRUD logic


---

## 1. Repository Structure & Naming

### Repository
- Repository name: **Train-Case**
  - Example: `Saucebucket-Inventory`
- Default branch: `main`
- **No direct commits to `main`** — all changes go through Pull Requests

## 2. Branch Naming Conventions

### Format

```
<change type>/<short-pull-request-description>
```

### Examples

```
feat/login-form
fix/null-pointer-on-save
refactor/inventory-service
docs/api-overview
chore/update-dependencies
```

### Rules

* Lowercase only
* Use hyphens (`-`), not underscores (`_`)
* Limit pull requests to one per branch except for documentation

---

## 3. Issues

Every issue **must** have:

* A milestone
* At least one label
* An assignee
* Clear acceptance criteria


### Issue Title Format

```
[Change Type] Short Description
```

#### Examples

```
[Feat] Implement login validation
[Fix] Fix incorrect stock decrement
[Tech-Debt] Add empty-state for product list
```

> If an issue does not have a milestone, it should not exist.

---

## 4. Milestones

Milestones represent **deliverable units** in sprint numbers, not vague goals.

### Examples

* `Sprint-1`
* `Sprint-2`

### Rule

* No issue exists without a milestone

---

## 5. Commit Message Format

Use a lightweight and clean commit style, use proper capitalization:

```
<Change Type>: Short, imperative description
```

### Examples

```
Feat: Add product creation endpoint
Fix: Prevent crash when inventory is empty
Refactor: Optimized CRUD logic
Docs: Update setup instructions
```

### Rules

* One logical change per commit
* Avoid vague messages like `misc`, `stuff`, or `updates`

### Notes

Combine multiple change tags together alphabetically for commit messages, separated by `&` with no spaces:

```
Build&Feat: Updated dependency toolchain
```

However, limit commit tags to a maximum of 3.

---

## 6. Pull Requests

### PR Title

Follow title format:

```
[Feat] Add inventory delete flow
```

### PR Description Must Include

* Which issue(s) it closes
* Build and test status

#### Example

```
Closes Issue #23
```

---

## 7. Code Review Rules

* At least **1 approval** required before merge
* Authors **cannot approve their own PRs**
* PRs must:

  * Build successfully
  * Pass basic tests (if applicable)

### Review Guidelines

* Focus on correctness, readability, and simplicity
* Avoid unnecessary formatting nitpicks
* Prefer suggestions over commands

---

## 8. General Rules of Thumb

* Small PRs are preferred over large PRs
* Split tasks that exceed ~2–3 days of work
* Ask before large refactors
* No force-push on shared branches
* Communication > perfection

---


