   M S B A M L   `   `   `  ����F  BTourPlanner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TourPlanner.ViewsTourPlanner.Views  5      U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 5      5clr-namespace:TourPlanner.EnumsTourPlanner.Enums  5      "    TourPlanner.Views.AddLogForm   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:TourPlanner.Views     5      ?System,clr-namespace:System;assembly=System.Runtime     5      1enumsclr-namespace:TourPlanner.Enums     5        � 	Resources  5         ButtonStyle �� 
TargetType �� Property  ButtonBackgroundColor �� Value  ButtonForegroundColor �� Triggers P� Property P� Value  ButtonHoverBackgroundColor  DifficultyProvider S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� TypeName  RatingProvider��%  &	        &	     &	 �    �� 5   
   " ��6$   .�5      ��  � ��# �  6+   �� 5       � ��# �  6+   �� 5       � ��# �  6,   �� 5       � ��$ 1q�60   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Segoe UI)�6+   �� 5       � ��$
 10,5q�6(   �� 5       �@�Cursor$
 Hand��6'   �� 5       �@�FocusVisualStyle 61   Z�  5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � ��# �  63   �� 5       � ��# �  64   5      5      S� 5   
   $ 	GetValues��68   "  6O   	 5      M� 5      $
 enums:Difficulty��6   5!      S� 5"   
   $ 	GetValues��64   "  6K   	 5#      M� 5$      $
 enums:Rating��6   5&      5'      .��5)      � $��10q�6     BackgroundColor#���  6    � ColumnDefinitions 5*   
   �� 5+      �� 5,       � RowDefinitions 5.   
   �� 5/      $
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
��Auto �6   �� 5:      $
��Auto �6   .+�5=   
   �� $��0��6   $��Date:��6   $��Center=�6-   �� 5>   
   $��0��6   $��1��6   $	��200��6/   $��Center=�6X   $��5q�6s     TextBoxBackgroundColor#���  6~     ForegroundColor#���  6�   ��6;   �� *NewLog.Date+6�   �� 5@   
   $��1��6   $��	Duration:��6   $��Center=�61   �� 5A   
   $��1��6   $��1��6   $	��200��6/   $��Center=�6]   $��5q�6x   #���  6�   #���  6�   ��6;   �� *NewLog.TotalTime+6�   �� 5C   
   $��2��6   $��	Distance:��6   $��Center=�61   �� 5D   
   $��2��6   $��1��6   $	��200��6/   $��Center=�6a   $��5q�6|   #���  6�   #���  6�   ��6;   �� *NewLog.TotalDistance+6�   �� 5G   
   $��3��6   $��Rating:��6   $��Center=�6/   �� 5H   
   $��3��6   $��1��6    $	��200��60   $��5q�6�    	 ComboBoxBackgroundColor#��� 	 6�    
 ComboBoxForegroundColor#��� 
 6�   ��6<   ��  �� Source# [  �� SelectedItem 6{   �� *NewLog.Rating+��5I      �� 5J      " ��6   .�5K      ��  � ��  ComboBoxItemBackgroundColor# �  63   �� 5L       � ��  ComboBoxItemForegroundColor# �  63    5M      P� 5N       �@��
IsSelected$
 True��68   . �5O      ��  � �� ' #ComboBoxItemSelectedBackgroundColor# �  6;   �� 5P       � ��
 �   �6;   5Q      5S      5U      �� 5X   
   $��4��6   $��Difficulty:��6   $��Center=�63   �� 5Y   
   $��4��6   $��1��6    $	��200��60   $��5q�6�   #��� 	 6�   #��� 
 6�   ��6<   �� # [  6   �� *NewLog.Difficulty+��5Z      �� 5[      " ��6   .�5\      ��  � ��# �  63   �� 5]       � ��# �  63    5^      P� 5_       �@��
IsSelected$
 True��68   . �5`      ��  � ��# �  6;   �� 5a       � ��
 �   �6;   5b      5d      5f      �� 5j   
   $��7��6   $��2��6"   $��0��66    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��5q�6|   .+�5k      �� $��Ok��6   $��80��6D   #��[  6O   $��0,0,10,0q�6t   ��6"   �� *AddLogCommand+6t   �� 5l      $��Cancel��6   $��80��6N   #��[  6Y   ��6&   �� *CancelAddLogCommand+6Y   5m      5n      5o      