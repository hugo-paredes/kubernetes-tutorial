#!/bin/bash

function write_header() {
    echo "----------------------------------------------------------------------------------"
    echo "$@"
    echo "----------------------------------------------------------------------------------"
    set -x
}

function white_space {
    set +x
    echo ""
    echo ""
}

write_header "Get git tag"
docker pull --quiet gittools/gitversion:5.3.5-linux-alpine.3.10-x64-netcoreapp3.1
git_version=$(docker run --rm -v "${PWD}":/data gittools/gitversion:5.3.5-linux-alpine.3.10-x64-netcoreapp3.1 /data /showvariable SemVer)
white_space

write_header "Build the Docker image"
docker build --rm -t shopping-mall:"${git_version}" ./api/src/Ordering.API/
white_space

write_header "Tag image with 'git tag' and 'latest'"
docker tag shopping-mall:"${git_version}" hugoparedes/shopping-mall:"${git_version}"
docker tag shopping-mall:"${git_version}" hugoparedes/shopping-mall:latest
white_space

write_header "Push both images"
docker push hugoparedes/shopping-mall:"${git_version}"
docker push hugoparedes/shopping-mall:latest
white_space
