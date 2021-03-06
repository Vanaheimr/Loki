#!/bin/bash

#------------------------------------------------------------
# Note: Windows users please use the MINGW32 shell for this!
#------------------------------------------------------------

if [ ! -d Vanaheimr ]; then

	if [ "$MSYSTEM" == "MINGW32" ]; then
		echo "Assuming a windows system";
		echo "As administrator please run: mklink /D Vanaheimr ..";
		echo "Afterwards plase re-run this script...";
		exit 1;
	else
		echo "Assuming a unix system";
		ln -s Vanaheimr ../
	fi;

else
	echo "Vanaheimr found...";
fi;

cd ..

# Basics

if [ ! -d Illias ]; then
	git clone git://github.com/Vanaheimr/Illias.git;
else
	echo "Illias found..."
fi;

if [ ! -d Styx ]; then
	git clone git://github.com/Vanaheimr/Styx.git;
else
	echo "Styx found..."
fi;


# Graphs

if [ ! -d Eunomia ]; then
	git clone git://github.com/Vanaheimr/Eunomia.git;
else
	echo "Eunomia found..."
fi;

if [ ! -d Blueprints.NET ]; then
	git clone git://github.com/Vanaheimr/Blueprints.NET.git;
else
	echo "Blueprints.NET found..."
fi;

if [ ! -d Walkyr ]; then
	git clone git://github.com/Vanaheimr/Walkyr.git;
else
	echo "Walkyr found..."
fi;

if [ ! -d Jurassic ]; then
	git clone git://github.com/Vanaheimr/Jurassic.git;
else
	echo "Jurassic found..."
fi;


# Networking
if [ ! -d Hermod ]; then
	git clone git://github.com/Vanaheimr/Hermod.git;
else
	echo "Hermod found..."
fi;

if [ ! -d Bifrost ]; then
	git clone git://github.com/Vanaheimr/Bifrost.git;
else
	echo "Bifrost found..."
fi;
