# Jornal Mobile Applicaton

The Journal mobile app.

## Overview

This is mobile application developed using Flutter.

## Getting Started

This section describes what is necessary fot running the project locally.

### Mandatory tools:

- NPM - Latest LTS version
- Visual Studio Code

## Localization/Internationalization

All strings must be added in the **app_*[languages]*.arb** files located under the directory **lib/l10n**.

## Configuring Visual Studio code
Create a new run task and add the following content in the file

{
	"version": "2.0.0",
	"tasks": [
		{
			"type": "npm",
			"script": "generate_l10n",
			"problemMatcher": [],
			"label": "generate_l10n",
		},
		{
			"type": "npm",
			"script": "genarate_auto_gen_files",
			"problemMatcher": [],
			"label": "genarate_auto_gen_files",
			"dependsOn":["generate_l10n"],
		}
	]
}


And add the prelunch task into the launch json:

"preLaunchTask": "genarate_auto_gen_files"