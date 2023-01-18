set -uC
cd ${0%/*}

rm -fr ./bin ./obj ./dist
dotnet publish -o ./dist
rm -fr ./dist/*.pdb