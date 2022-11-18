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

## Configuring Visual Studio Code

Inside the **vscode_folder_example** directory you will find an example file for the ***launch.json*** and another file for ***tasks.json***. 

These files contain the suggested configuration to make the run/debug process from Visual Studio Core simpler as you don't neeed to remember of executing the commands to generate the l10n files and the build_runner to generate the models serialization methods.

## Creating models
All models created must have the **@JsonSerializable()** annotation from the package **json_annotation** 

All model classes must extend from **Serializable** class.

All models classes files must have the following code righ after the import section

``` dart
part 'generated/file_name.g.dart';
```

Notice that the ***file_name*** must be replaced by the file name of the file you are writing.

You will get errors when you create new models as the generated filed are not created yet. To remove these errors run the **genarate_auto_gen_files** NPM script. It is responsible to invoke the build_runner too and generate all auto generated not onle the models files but also other fiels needed to the project.

and the following methods:

``` dart
@override
  Map<String, dynamic> toJson() => _$ClassNameToJson(this);

  factory ClassName.fromJson(Map<String, dynamic> json) =>
      _$ClassNameFromJson(json);
}
```

Notice that the ***ClassName*** must be replaced by the class name of the class you are writing.