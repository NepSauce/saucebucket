# Universal Codebase Navigation & Debugging Guide
*Works for any project, any language, any framework*

## **Finding Things in Any Codebase**

### The Universal Search Strategy

**1. Start with what you see or know**
- Error message? Search the exact text in quotes
- UI element? Search the visible text
- Feature name? Search variations: `featureName`, `feature-name`, `feature_name`

**2. Search patterns that work everywhere:**
```bash
# In VS Code / Any IDE
Cmd+Shift+F (Mac) / Ctrl+Shift+F (Windows)

# From terminal
grep -r "search term" .
grep -r "search term" --include="*.cs" --include="*.yml"  # Specific file types

# Case-insensitive
grep -ri "search term" .
```

**3. Use multiple search terms**
- Don't find it with "login"? Try: "sign in", "authenticate", "auth"
- Think like the developer: What would THEY call this?

### Understanding Project Structure (Any Project)

**Common patterns to look for:**

```
src/ or app/           → Main source code
  components/          → Reusable UI pieces
  pages/ or views/     → Full page components
  routes/ or routing/  → URL routing
  api/ or services/    → Backend communication
  utils/ or helpers/   → Utility functions
  hooks/               → React hooks (if React)
  store/ or state/     → State management
  
config/                → Configuration files
public/ or static/     → Static assets (images, fonts)
tests/ or __tests__/   → Test files
dist/ or build/        → Compiled output (DON'T EDIT)
node_modules/          → Dependencies (DON'T EDIT)
```

**File naming hints:**
- `.test.js`, `.spec.js` → Tests
- `.config.js` → Configuration
- `index.js` → Entry point or barrel export
- `.d.ts` → TypeScript type definitions
- `.stories.js` → Storybook stories

## **Universal Debugging Process**

### Step 1: Reproduce Reliably
```
1. What are the EXACT steps?
2. Does it happen every time?
3. Does it happen in incognito/different browser?
4. What's the error message (if any)?
```

### Step 2: Locate the Problem

**For UI/Visual Issues:**
1. Right-click element → Inspect
2. Note the class names, IDs, data attributes
3. Search codebase for those identifiers
4. Find the component that renders it

**For Logic/Data Issues:**
1. Add `console.log()` everywhere (seriously)
2. Check browser console for errors
3. Use debugger: Add `debugger;` statement in code
4. Step through execution line by line

**For Build/Dependency Issues:**
1. Delete `node_modules` and `package-lock.json`
2. Run `npm install` or `yarn install` or `pnpm install`
3. Clear cache: `npm cache clean --force`
4. Check Node version matches project requirements

### Step 3: Understand the Code Flow

**Trace backwards from problem:**
```
Error/Bug Location
    ↑
What calls this?
    ↑
What triggers that?
    ↑
User action / Event / Data source
```

**How to trace:**
```javascript
// Add logs with context
console.log('🔵 Function called with:', arguments);
console.log('🟢 State before update:', state);
console.log('🔴 Error occurred:', error);

// Use stack traces
console.trace('How did we get here?');

// Check what changed
console.log('Previous:', previousValue, 'Current:', currentValue);
```

## 🔧 **Common Problems & Solutions**

### Problem: "It works on my machine"
**Solutions:**
- Check environment variables (`.env` files)
- Compare Node/npm versions: `node -v`, `npm -v`
- Check if running dev vs production build
- Clear browser cache & localStorage
- Try in incognito mode

### Problem: Can't find where something is defined
**Search techniques:**
```bash
# Exact phrase
"export const UserProfile"

# RegEx for flexibility
(function|const|let|class) UserProfile

# Find imports
import.*UserProfile
from.*user.*profile

# Find where it's used
UserProfile\(
<UserProfile
```

### Problem: Changes don't appear
**Checklist:**
- [ ] Did you save the file? (Check for dot in tab)
- [ ] Did dev server reload? (Check terminal)
- [ ] Hard refresh browser: `Cmd+Shift+R` / `Ctrl+Shift+F5`
- [ ] Check if editing correct file (might be duplicate names)
- [ ] Clear build cache: `rm -rf dist/ .cache/`

### Problem: "Cannot find module" or import errors
**Solutions:**
1. Check the path is correct (relative vs absolute)
2. Check file extension (`.js` vs `.jsx` vs `.ts`)
3. Run `npm install` / `yarn` / `pnpm install`
4. Check `package.json` for the dependency
5. Restart dev server

### Problem: Code runs but behaves wrong
**Debug approach:**
```javascript
// 1. Verify input
console.log('Input received:', input);

// 2. Check conditions
console.log('Should this run?', condition);
if (condition) {
  console.log('✅ Condition true, executing...');
} else {
  console.log('❌ Condition false, skipping...');
}

// 3. Verify output
console.log('Result:', result);
console.log('Expected:', expectedResult);
```

## 📖 **Reading Unfamiliar Code**

### The 3-Pass Method

**Pass 1: Skim for structure (5 min)**
- What files exist?
- What are the main folders?
- Find entry point (usually `index.js`, `main.js`, `app.js`)

**Pass 2: Follow one feature (15 min)**
- Pick ONE thing you understand (e.g., login button)
- Trace it: Button → Handler → API call → Response → Update
- Don't get distracted by other code

**Pass 3: Read related code (30 min)**
- Now read files you discovered in Pass 2
- Look at tests - they show intended usage
- Read comments and documentation

### Reading a New File

**Order to read:**
1. **Exports** - What does this file provide?
2. **Imports** - What does it depend on?
3. **Type/Interface definitions** - What shape is the data?
4. **Main function/component** - What's the core logic?
5. **Helper functions** - How does it work?

**Example:**
```javascript
// 1. EXPORTS - This file provides a UserList component
export default function UserList() {

  // 2. What data does it use?
  const users = useUsers(); // <- Find this next
  
  // 3. What does it render?
  return (
    <div>
      {users.map(user => <UserCard key={user.id} user={user} />)}
    </div>
  );
}

// Now find: Where is useUsers()? What is UserCard?
```

## 🛠️ **Making Changes Safely**

### The Safe Change Process

**1. Understand before changing**
- Read the code you're about to change
- Search where else it's used: "Find All References"
- Check if tests exist for it

**2. Make minimal changes**
```bash
# Bad: Change 10 things at once
# Good: Change 1 thing, test, commit, repeat
```

**3. Test immediately**
- Run the app: Does it still work?
- Test your change: Does it do what you want?
- Run automated tests: `npm test`

**4. Commit logically**
```bash
# See what changed
git diff

# Add specific files
git add file1.js file2.js

# Or add interactively (review each change)
git add -p

# Commit with clear message
git commit -m "fix: prevent double selection in navigation menu"
```

### Version Control Superpowers

**Before you change anything:**
```bash
# Create a branch
git checkout -b fix/my-bug-fix

# Now you can experiment safely!
# If it breaks, just switch back:
git checkout main
```

**Time travel through code:**
```bash
# See history of a file
git log -p filename.js

# See who wrote this line and when (blame)
git blame filename.js

# See what changed in last commit
git show

# Undo uncommitted changes
git checkout -- filename.js
```

## 💡 **Universal Pro Tips**

### 1. **Master Your Search**
- `Cmd+P` / `Ctrl+P` → Quick open file by name
- `Cmd+Shift+O` / `Ctrl+Shift+O` → Jump to symbol in file
- `Cmd+Click` on function → Go to definition
- `Shift+F12` → Find all references

### 2. **Learn the Keyboard Shortcuts**
Every IDE has them. Learn 5 per week:
- Open file
- Search in files
- Go to definition
- Format document
- Toggle terminal

### 3. **Use the Browser DevTools**
```
F12 or Cmd+Opt+I → Open DevTools

Elements tab → Inspect HTML/CSS
Console tab  → See logs & errors
Network tab  → See API requests
Sources tab  → Debug JavaScript
Application  → See localStorage, cookies
```

### 4. **Comment Your Debugging**
```javascript
// TODO: Figure out why this is undefined
console.log('DEBUG:', mysteryVariable);

// FIXME: This breaks when user is null
if (user) { ... }

// NOTE: This workaround needed because of X
```

### 5. **When Completely Stuck**
1. **Take a break** - 15 min walk clears your head
2. **Rubber duck** - Explain problem out loud
3. **Search the exact error** - Someone else hit this
4. **Read docs** - Framework/library official docs
5. **Ask for help** - Stack Overflow, Discord, colleague

## 📚 **Learning New Codebases**

### Week 1: Orientation
- [ ] Read README.md
- [ ] Get it running locally
- [ ] Change one small thing (color, text)
- [ ] Find where one feature is implemented

### Week 2: Exploration
- [ ] Fix one small bug
- [ ] Add one small feature
- [ ] Write/update one test
- [ ] Review someone else's code

### Month 1: Contribution
- [ ] Understand architecture patterns used
- [ ] Know where to find things quickly
- [ ] Confident making changes
- [ ] Can help onboard others

## 🎯 **Mental Models**

### "Everything is a box in a box"
```
Application
  └─ Features
      └─ Components
          └─ Elements
```
Find the right box, ignore the rest.

### "Follow the data"
```
User Input → Validation → Processing → Storage → Display
```
Where does data come from? Where does it go?

### "Code is communication"
Good code tells you:
- What it does (function names, comments)
- How to use it (examples, tests)
- Why it exists (comments, git history)

## 🚀 **Quick Reference**

### When you're stuck...
1. ❓ What am I trying to do?
2. 🔍 Where is the relevant code?
3. 📖 What does this code do?
4. 🐛 What's different from expected?
5. 🔧 What's the minimal change?
6. ✅ How do I verify it worked?

### Search checklist
- [ ] Searched exact error message
- [ ] Searched feature/component name
- [ ] Searched in files (Cmd+Shift+F)
- [ ] Checked git history
- [ ] Looked at tests
- [ ] Grepped in terminal

### Before asking for help
- [ ] Can describe the problem clearly
- [ ] Know the exact steps to reproduce
- [ ] Tried at least 3 things
- [ ] Read error message completely
- [ ] Googled the error
- [ ] Checked recent changes (git)

---

## 🎓 **Remember**

> "The only way to learn a new codebase is to work in it."

Every expert started exactly where you are. The difference is time and practice.

**You got this!** 💪

---

### Quick Win: Your Next Debugging Session

1. Pick a small bug or feature
2. Use "Find in Files" to locate relevant code
3. Add `console.log` to trace execution
4. Make ONE small change
5. Test it
6. Commit it

Repeat. That's how professionals do it too.
