#!/bin/sh
#
# An example hook script to check the commit log message.
# Called by "git commit" with one argument, the name of the file
# that has the commit message.  The hook should exit with non-zero
# status after issuing an appropriate message if it wants to stop the
# commit.  The hook is allowed to edit the commit message file.
#
# To enable this hook, rename this file to "commit-msg".

# Uncomment the below to add a Signed-off-by line to the message.
# Doing this in a hook is a bad idea in general, but the prepare-commit-msg
# hook is more suited to it.
#
# SOB=$(git var GIT_AUTHOR_IDENT | sed -n 's/^\(.*>\).*$/Signed-off-by: \1/p')
# grep -qs "^$SOB" "$1" || echo "$SOB" >> "$1"

# This example catches duplicate Signed-off-by lines.

test "" = "$(grep '^Signed-off-by: ' "$1" |
	sort | uniq -c | sed -e '/^[ 	]*1[ 	]/d')" || {
	echo >&2 Duplicate Signed-off-by lines.
	exit 1
}
if ! head -1 "$1" | grep -qE "^(feat|fix|ci|chore|docs|test|style|refactor|chk|merge)(\(.+?\))?: .{1,}$"; then
    echo "Aborting commit. Your commit message is invalid. See some examples below:" >&2
    echo "feat(logging): added logs for failed signups" >&2
    echo "fix(homepage): fixed image gallery" >&2
    echo "test(homepage): updated tests" >&2
    echo "docs(readme): added new logging table information" >&2
    echo "For more information check https://www.conventionalcommits.org/en/v1.0.0/ for more details" >&2
    exit 1
fi
if ! head -1 "$1" | grep -qE "^.{1,100}$"; then
    echo "Aborting commit. Your commit message is too long." >&2
    exit 1
fi