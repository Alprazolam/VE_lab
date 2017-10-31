# General ideas about git
[Some good (also long) stuff to read.](https://git-scm.com/book/en/v2/Getting-Started-Git-Basics)  

Some points from my point of view:
* Git stores the files in tree like structures. Precisely git tracks down the changes rather than versions of files.
* Git file tree is stored both locally and remotely. The same branches (by name) are supposed to be synced but no every local branch is necessarily 
existing on remote server. You will need to explicity "push" your a new branch to remote server if you wish to do so.
* <b>Do not play with master branch.</b> It is supposed to be correctly built/run all the time.

# Start with some git commands
Before starting. [Look here always.](https://git-scm.com/docs)

```
git clone <address>
```
This will clone the file tree from the remote address you provided. 
You might need your git user name and password for private repos (like this one).
<i>Optionally you can set up RSA keys at your settings page. Read more [here](https://help.github.com/articles/connecting-to-github-with-ssh/).</i>


```
git status
```
A good habit is always check the status of your repo.


``` 
git checkout <branch_name>
```
You will switch to the branch designated by <i>branch_name</i>.
```
git checkout -B <branch_name>
```
You will switch to the branch designated by <i>branch_name</i>. With ```-B``` you will create <i>branch_name</i> if it does not exist.


# Typical workflow
Assume you have made some changes to the existing files on a branch. (Assume nobody updates anything on remote in the mean time)
```
git add <files you changed>
git commit -m "<your commit message>"
git push
```
* You need to first add files that you changed, using ```git add -u``` will add all files you have changed (shown red in ```git status```) so you 
do not need to add them one by one.
* Second step you can commit by ```git commit``` but then you will get to change/confirm the commit log in a vim pop up window, that scares off a lot
of people. You can always (literally) type ```:wq``` to quit in this case. There is a way to change the default editor for git commit message, and I 
recommend ```nano``` for modern users.
* Then you push to remote server.

# More to check
* If someone has changed the same branch or if you are merging branches (it is bad to use merge of course). You might get conflits. [Read here how 
to resolve conflicts.](https://help.github.com/articles/resolving-a-merge-conflict-using-the-command-line/)
* Try to use ```rebase``` all the time instead of ```merge```.
