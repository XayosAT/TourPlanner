   M S B A M L   `   `   `  ����F  BTourPlanner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TourPlanner.ViewsTourPlanner.Views  5      ?$clr-namespace:TourPlanner.ViewModelsTourPlanner.ViewModels  5      7 clr-namespace:TourPlanner.ModelsTourPlanner.Models  5      ?$clr-namespace:TourPlanner.ValidationTourPlanner.Validation  5	      U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 5
      5clr-namespace:TourPlanner.EnumsTourPlanner.Enums  5      #    TourPlanner.Views.AddTourForm   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:TourPlanner.Views     5      ;
viewModels$clr-namespace:TourPlanner.ViewModels     5      3models clr-namespace:TourPlanner.Models     5      ;
validation$clr-namespace:TourPlanner.Validation     5	      ?System,clr-namespace:System;assembly=System.Runtime     5
      1enumsclr-namespace:TourPlanner.Enums     5        � 	Resources  5         ButtonStyle �� 
TargetType �� Property  ButtonBackgroundColor �� Value  ButtonForegroundColor �� Triggers P� Property P� Value  ButtonHoverBackgroundColor  TransportTypeProvider S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� TypeName��%�  &	        &	     �� 5   
   " ��6$   .�5      ��  � ��# �  6+   �� 5       � ��# �  6+   �� 5       � ��# �  6,   �� 5       � ��$ 1q�60   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Segoe UI)�6+   �� 5       � ��$
 10,5q�6(   �� 5       �@�Cursor$
 Hand��6'   �� 5       �@�FocusVisualStyle 61   Z�  5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � ��# �  63   �� 5       � ��# �  64   5      5       S� 5!   
   $ 	GetValues��6;   "  6R   	 5"      M� 5#      $
 enums:TransportType��6   5%      5&      .��5(      � $��10q�6    � ColumnDefinitions 5)   
   �� 5*      �� 5+       � RowDefinitions 5-   
   �� 5.      $
��Auto �6   �� 5/      $
��Auto �6   �� 50      $
��Auto �6   �� 51      $
��Auto �6   �� 52      $
��Auto �6   �� 53      $
��Auto �6   �� 54      $
��Auto �6   �� 55      $
��Auto �6   �� 56      $
��Auto �6   �� 57      $
��Auto �6   �� 58      $
��Auto �6   �� 59      $
��Auto �6   .+�5<   
   �� $��0��6   $��Name:��6   $��Center=�6-   �� 5=   
   $��0��6   $��1��6   $	��200��6/   $��Center=�6;   $��5q�6V     TextBoxBackgroundColor#���  6a     ForegroundColor#���  6�   .��6�   ��5>      �� 5?       �� Path$ NewTour.Name�6    �� UpdateSourceTrigger$ PropertyChanged=�6.    �� ValidationRules 5@      3   -TourPlanner.Validation.TourNameValidationRule  5A      5C      .��5D      ��5E      �� 5F      " ��6    5G      P� 5H       �@=�HasError$
 True��6A   . �5I      ��  �@�ToolTip 68   ��  �� RelativeSource �� *Self+$&  (Validation.Errors)/ErrorContent�5J      5L      .��5M      5N      �� 5P   
   $��1��6   $��0��6   $��Description:��6-   $��Center=�6D   �� 5Q   
   $��1��6   $��1��6   $	��200��6/   $��Center=�6`   $��5q�6{   #���  6�   #���  6�   ��6;   �� *NewTour.Description+6�   �� 5S   
   $��2��6   $��0��6   $��Start Location:��6-   $��Center=�6G   �� 5T   
   $��2��6   $��1��6   $	��200��6/   $��Center=�6;   $��5q�6V   #���  6a   #���  6�   .��6�   ��5U      �� 5V      $ NewTour.StartLocation�6   $ PropertyChanged=�67    5W      7   1TourPlanner.Validation.TourLocationValidationRule  5X      5Z      .��5[      ��5\      �� 5]      " ��6    5^      P� 5_       �@=�HasError$
 True��6A   . �5`      ��  �@�ToolTip 68   ��  �� *Self+$&  (Validation.Errors)/ErrorContent�5a      5c      .��5d      5e      �� 5g   
   $��3��6   $��0��6   $��End Location:��6-   $��Center=�6E   �� 5h   
   $��3��6   $��1��6   $	��200��6/   $��Center=�6;   $��5q�6V   #���  6a   #���  6�   .��6�   ��5i      �� 5j      $ NewTour.EndLocation�6   $ PropertyChanged=�65    5k        5l      5n      .��5o      ��5p      �� 5q      " ��6    5r      P� 5s       �@=�HasError$
 True��6A   . �5t      ��  �@�ToolTip 68   ��  �� *Self+$&  (Validation.Errors)/ErrorContent�5u      5w      .��5x      5y      �� 5{   
   $��4��6   $��0��6   $��Transport Type:��6-   $��Center=�6G   �� 5|   
   $��4��5~      $��1��6    $	��200��60   $��Center=�6<   $��5q�6W     ComboBoxBackgroundColor#���  5        ComboBoxForegroundColor#���  6J   ��5|      ��  �� Source# [  �� SelectedItem 5}      �� *NewTour.Type+ �� Mode$ TwoWay=�5�      �� 5�   
   $��7��6   $��2��6"   $��0��66    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��5q�6|   .+�5�      �� $��Ok��6   $��80��6E   #��[  6P   $��0,0,10,0q�6u   ��6"   �� *AddTourCommand+6u   �� 5�      $��Cancel��6   $��80��6O   #��[  6Z   ��6&   �� *CancelAddTourCommand+6Z   5�      5�      5�      