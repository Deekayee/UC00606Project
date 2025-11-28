# Git Workflow & Team Guidelines

## 🎯 Our Workflow Overview

1. Pick an issue from the project board
2. Create a branch for that issue
3. Work on your code
4. Commit your changes regularly
5. Push your branch and create a Pull Request
6. Get it reviewed
7. Merge to main

---

## 📋 Before You Start Coding

### 1. Always start from the latest main branch

```bash
git checkout main
git pull origin main
```

### 2. Create a new branch for your work

**Branch naming convention:**
- Features: `feature/issue-number-short-description`
- Bugs: `fix/issue-number-short-description`
- Documentation: `docs/what-you-are-documenting`

**Examples:**
```bash
git checkout -b feature/3-user-login
git checkout -b fix/7-save-button
git checkout -b docs/api-documentation
```

**Why this matters:** Keeps work organized and makes it clear what each branch does.

---

## 💾 While You're Working

### Commit early and often

**Good commit messages:**
- Start with a verb: "Add", "Fix", "Update", "Remove"
- Be specific: what did you actually do?
- Reference the issue number with `#`

**Examples:**
```bash
git commit -m "Add login form UI #3"
git commit -m "Fix save button not responding #7"
git commit -m "Update user model with email field #3"
```

### Push your branch regularly

```bash
git push origin feature/3-user-login
```

**Why:** Acts as a backup and lets teammates see your progress.

---

## 🔄 Creating a Pull Request (PR)

### When you're ready for review:

1. **Push your latest changes**
   ```bash
   git push origin your-branch-name
   ```

2. **Go to GitHub** → your repository → you'll see a "Compare & pull request" button

3. **Fill out the PR template:**
   - **Title:** Clear description of what you did
   - **Description:** 
     - What issue does this close? Use `Closes #issue-number`
     - What did you change?
     - Anything the reviewer should pay attention to?
   
   **Example:**
   ```
   Closes #3
   
   Added user login functionality:
   - Created login form UI
   - Implemented authentication logic
   - Added error handling for invalid credentials
   
   Please test with both valid and invalid login attempts.
   ```

4. **Request a review** from at least one teammate

5. **Assign yourself** to the PR

---

## 👀 Code Review Process

### If you're the reviewer:

- **Be constructive and kind** - we're all learning
- **Check:**
  - Does the code work?
  - Is it readable?
  - Does it follow our coding standards?
  - Are there obvious bugs?
- **Leave comments** on specific lines if something needs clarification
- **Test it if possible** - pull the branch and run it locally
- **Approve or Request Changes**

### If you're the author:

- **Respond to feedback** professionally
- **Make requested changes** and push them (they'll appear in the same PR)
- **Don't take it personally** - reviews make everyone better
- **Ask questions** if you don't understand feedback

---

## ✅ Merging

### Who can merge:

- **Only merge your own PR after:**
  - At least one teammate has approved it
  - All requested changes are addressed
  - No merge conflicts exist

### How to merge:

1. Click **"Merge pull request"** on GitHub
2. Choose **"Squash and merge"** (keeps history clean) or **"Create a merge commit"** (preserves all commits)
3. **Delete the branch** after merging (GitHub will prompt you)

### After merging:

Update your local main branch:
```bash
git checkout main
git pull origin main
```

---

## 🚨 Common Issues & Solutions

### "I have merge conflicts!"

**Don't panic.** This happens when someone else changed the same files.

1. Make sure your work is committed
2. Get the latest main:
   ```bash
   git checkout main
   git pull origin main
   ```
3. Go back to your branch:
   ```bash
   git checkout your-branch-name
   ```
4. Merge main into your branch:
   ```bash
   git merge main
   ```
5. Git will tell you which files have conflicts
6. Open those files, look for `<<<<<<<`, `=======`, `>>>>>>>` markers
7. Fix the conflicts manually (keep what's needed, remove markers)
8. Commit the resolution:
   ```bash
   git add .
   git commit -m "Resolve merge conflicts with main"
   git push origin your-branch-name
   ```

### "I accidentally committed to main!"

```bash
# Don't push! First, create a branch with your changes:
git branch feature/your-feature-name

# Reset main to match the remote:
git checkout main
git reset --hard origin/main

# Go to your new branch:
git checkout feature/your-feature-name

# Now push it properly:
git push origin feature/your-feature-name
```

### "I need to update my branch with changes from main"

```bash
git checkout main
git pull origin main
git checkout your-branch-name
git merge main
# Resolve any conflicts if they appear
git push origin your-branch-name
```

---

## 📏 Coding Standards

### C# Naming Conventions

- **Classes/Methods:** `PascalCase` → `UserController`, `GetUserData()`
- **Variables/parameters:** `camelCase` → `userName`, `isValid`
- **Private fields:** `_camelCase` → `_userData`, `_isLoggedIn`
- **Constants:** `UPPER_SNAKE_CASE` → `MAX_LOGIN_ATTEMPTS`

### Code Quality

- **Write comments** for complex logic
- **Keep methods short** - if it's more than 30 lines, consider breaking it up
- **Use meaningful names** - `getUserById()` not `get()`
- **Remove commented-out code** before committing
- **No hardcoded values** - use constants or config files

### Before You Commit

- [ ] Code compiles without errors
- [ ] You've tested your changes
- [ ] No `Console.WriteLine()` debug statements left behind
- [ ] Code follows naming conventions
- [ ] Comments explain the "why", not the "what"

---

## 🎯 Issue Management

### Creating Issues

1. **Clear title:** "Implement user registration" not "do the thing"
2. **Description should include:**
   - What needs to be done?
   - Why is this needed?
   - Any technical details or constraints?
   - Acceptance criteria (what does "done" look like?)
3. **Assign:** Pick someone or yourself
4. **Label it:** `feature`, `bug`, `documentation`, `high-priority`, etc.
5. **Add to milestone** if applicable
6. **Move to correct column** in project board

### Working on Issues

- **Assign yourself** before starting work (prevents duplicate work)
- **Move the issue card** on the project board: Todo → In Progress → Review → Done
- **Update the issue** if you run into blockers or need help
- **Link your PR** to the issue using `Closes #issue-number` in the PR description

---

## 🙋 Getting Help

### When you're stuck:

1. **Check this document** first
2. **Google the error message** - Stack Overflow is your friend
3. **Ask in the team chat** - describe what you tried
4. **Create a comment on your issue** - tag someone who might know
5. **Schedule a quick call** - sometimes 5 minutes of screenshare beats an hour of typing

### When to ask for help:

- ✅ After you've tried to solve it yourself for 30+ minutes
- ✅ When you're not sure if your approach is correct
- ✅ When you've broken something and don't know how to fix it
- ✅ When you need clarification on requirements

---

## 📚 Useful Git Commands Cheat Sheet

```bash
# See what branch you're on and what's changed
git status

# See all branches
git branch -a

# Switch branches
git checkout branch-name

# Create and switch to a new branch
git checkout -b new-branch-name

# Stage changes for commit
git add .                  # Add everything
git add specific-file.cs   # Add one file

# Commit changes
git commit -m "Your message here"

# Push to GitHub
git push origin branch-name

# Pull latest changes
git pull origin main

# See commit history
git log --oneline

# Undo last commit (keeps changes)
git reset --soft HEAD~1

# Discard all local changes (careful!)
git reset --hard HEAD
```

---

## 🎓 Remember

- **Communication is key** - when in doubt, ask
- **Commit often** - small commits are easier to review and debug
- **Review kindly** - we're all learning together
- **Test your code** before creating a PR
- **Don't rush merges** - better to catch issues in review than in production
- **Have fun!** This is a learning experience 🚀

---

**Questions or suggestions for this guide?** Open an issue labeled `documentation` or bring it up in our next team meeting!