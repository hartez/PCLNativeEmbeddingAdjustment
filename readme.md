# Native Embedding PCL Example

This is a quick example project to demonstrate some options for modifying native control properties if you're using XAML to embed native controls into a Xamarin.Forms PCL project.

This example applies if you're doing something similar to [Pierce Boggan's example](https://blog.xamarin.com/adding-bindable-native-views-directly-to-xaml/) and you realize that you need to modify some of the native properties of your embedded control. The example only demonstrates this for Android, but the principle is the same on any platform.

## Option 1

Instead of embedding your native control directly in the XAML, drop in a placeholder control (e.g., a ContentView) and add have the PCL project request that the native project fill it in. In this project, we're using the MessagingCenter to make the request. The PCL sends a message out with the ContentView to be filled in; the Android Activity responds to that message by generating the native control (and setting any relevant properties), then wrapping it in a NativeViewWrapper. 

This is pretty similar to what you'd do if you were creating your native controls entirely in C# (as shown in the [Native Embedding documentation](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/add-platform-controls/)). It's easy to mix-and-match this with regular XAML embedding, so you only have to write the extra code for the controls/platforms which need it.

## Option 2

Go ahead and embed the native control using XAML, then ship the wrapped control to the native project and make your native property adjustments there. The control will be wrapped in a `NativeViewWrapper`, and you can get access to the native control via the `NativeView` property. Again, we're using MessagingCenter to ship the control between the PCL and native project.