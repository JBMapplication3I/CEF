#!/bin/sh
# (Other than frontend stuff and solution items) clean all bin and obj folders
# to give a truly "clean build".
set -e

rm -r Plugins/
git restore Plugins/

find . -type d                                                      \
    \( -name obj -or -name bin \)                                        \
    -and                                                                 \
    \( -not -path './ThirdParty/*' -and -not -path '*/node_modules/*' -and -not -path '.git/*' \) \
    -exec rm -r '{}' +
