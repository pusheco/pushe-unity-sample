# Pushe Plus unity plugin

## How to setup

> Plugin uses Gradle. So in your build tool, select Gradle.

- If you don't have a `mainTemplate.gradle` in your project, copy this file from here to `Assets/Plugins/Android`.
- If you don't have a `AndroidManifest.xml` in your project, copy this file from here to `Assets/Plugins/Android` replece **PLUS_TOKEN** with your own.

> For simple usage you don't need to use any scripts. Simply run the project.

## Use with JarResolver?

If you really don't want to use `Gradle template`, add `co.pushe.plus:unity-extended:0.4.4` as an android package in a `*Dependencies.xml` file.


## More info

For further information chackout the documentations.

You can also see some actions in the sample code, `SampleCode.cs`.