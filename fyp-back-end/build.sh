rm -rf node_modules
cd ..
zip -r back-end.zip fyp-back-end/*
rsync -avzh ./back-end.zip long-test:~/
rm back-end.zip
ssh long-test "sudo rm -r fyp-back-end && sudo unzip back-end.zip && cd fyp-back-end && sudo yarn install && forever restart -c 'yarn serve' ./ || forever start -c 'yarn serve' ./"
