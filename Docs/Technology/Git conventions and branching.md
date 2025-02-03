# Git conventions and branching

#Commits
To write a good commit message, you should follow these guidelines:

* Include Relevant Context: Mention any relevant issue numbers, feature requests, or discussions related to the changes to help others understand the motivation behind the commit.

* Organize Commits Logically: Break down large features or fixes into smaller, logical commits where each commit represents a self-contained unit of work.

* Use Imperative Verbs: Write commit messages in the imperative mood, starting with a verb that describes what the commit does, such as "Add," "Fix," "Update," or "Refactor".

* Capitalize the Subject Line: Start the subject line with a capital letter for consistency.

* Don't End the Subject Line with a Period: Avoid trailing punctuation in the subject line to save space and make the message cleaner.

* Keep Subject Line Short: Limit the subject line to 50 characters or less. This ensures that the commit history remains readable even when viewed in narrow terminal windows.
* Use the Body to Explain What and Why: In the body of the commit message, explain what the commit does and why it is necessary, rather than focusing on how it is done. This provides valuable context for anyone looking at the commit later.
* Consider Using Gitmoji: Optionally, you can prefix your commit message with a gitmoji to visually indicate the type of change. This is particularly popular in projects using GitHub.

Here's an example commit message structure:

```
feat: add login functionality

This commit adds the ability for users to log in to the application. Related to issue #123.

- Implement user authentication logic
- Add login form UI component
```

Remember to replace feat with the appropriate keyword that reflects the nature of the commit (fix, docs, style, refactor, perf, test, build, or chore), followed by a colon and a space. 

Then, write a short description of the change, keeping it under 50 characters if possible. After that, you can include a blank line and then a more detailed explanation if needed, wrapping at around 72 characters per line.

# Branching

To be able to work as a team with git, we need to define a branching strategy. We will be using **trunk-based development** or **TBD.**

##What is trunk-based development?
Trunk-Based Development (TBD) is a software development strategy where developers merge smaller changes into the main codebase frequently and work directly on the trunk copy, rather than creating long-lived feature branches. This model encourages frequent commits to the main branch, facilitating continuous integration and delivery.

Key aspects of TBD include:

* Single Main Branch: Developers work on a single branch, typically referred to as 'trunk' or 'main', which serves as the primary source of truth for the codebase.
* Frequent Commits: Developers commit their changes to the trunk multiple times a day, ensuring that the codebase remains releasable on demand.
* Short-Lived Feature Branches: Although feature branches are used, they are expected to be short-lived and the result of a single person's work. This contrasts with long-lived feature branches seen in models like GitFlow.
* Merge Frequently: Merges are performed more often, reducing the risk of large-scale merge conflicts and making the process smoother.
* Continuous Delivery: TBD supports continuous delivery, allowing for rapid deployment of new features and bug fixes.
* Feature Flags: Feature flags are used to manage new features and changes, allowing for controlled rollout and quick removal if issues arise.

TBD is particularly suited for environments where continuous integration is desirable, and it enables teams to adapt to changing requirements quickly. It's not a new model; it has been practiced since the 1980s and is used at scale by large organizations like Google and Facebook 1.

The practice of TBD is often compared to other branching models such as GitFlow. While both involve feature branches, TBD emphasizes the simplicity of its version control strategy combined with a more complex coding style to manage work in progress. In contrast, GitFlow uses a version control strategy to isolate work, with new features isolated in their own branches.

In summary, TBD promotes a streamlined development process that aligns well with Agile and DevOps principles, focusing on frequent integration and delivery of code changes.

## Basic steps when developing using TBD
1. Find a story on the board you will work on.
2. From the master branch, create a new branch names features/<short description>
e.g. features/create-user.
3. Commit and push the changes you make to this feature branch. Make sure your commits follow the recommended structure as shown under the _commits_ header above.
4. Once work on the feature is finished, push and create a pull request from your branch to master.
5. In the PR, you will be asked to add a ticket as reference. Use the id for the story you worked on.
6. Your PR will be reviewed. Resolve any comments and once approved, merge the code into the master branch.
7. Done!

⚠️ Always make sure you have pulled the latest version of master before creating a new feature branch! ⚠️

To learn everything about trunk-based development go here: https://trunkbaseddevelopment.com/

##Branch naming
We use the following convention when naming feature branches: feature/<short description>
