first you need to tag:

git tag -a v0.1 -m v0.1

then you can use git menu items to push quickly and with current version

git push - standard add all, commit, push sequence (requires message input)
git quickPush - as normal push but with generated commit message 
git major tag - tags next major verion of the project (changes v0.1.0 to v1.0.0)
git minor tag - tags nex minor version change (changes v0.1.0 to v0.2.0)