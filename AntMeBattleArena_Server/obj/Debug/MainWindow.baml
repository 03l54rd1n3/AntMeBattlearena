   M S B A M L   `   `   `  ����R  NAntMeBattleArena_Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AntMeBattleArena_ServerAntMeBattleArena_Server  5   	   (    "AntMeBattleArena_Server.MainWindow   5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f6src,http://metro.mahapps.com/winfx/xaml/controls  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AntMeBattleArena_Server     5   	   ;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   -   5          � Title$  FHDW AntMe! Battle Arena��5
   	   $	��480��6*   $	��900��67   ���#21376A   AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   6X    � 	Resources 5      /   )AntMeBattleArena_Server.FontSizeConverter  fontSizeCon��%   &	         5   
   6"   5      .��5      � $��5,5,5,5q�6   
������� � RowDefinitions 5   
   �� 5      $��8* �6   �� 5      $��3* �6   �� 5      $��8* �6   �� 5      $��3* �6   �� 5      $��8* �6   �� 5      $��3* �6   �� 5      $	��67* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   (� $��1��6   $��2��6"   .��5       �� $
��Wrap=�6   $��Server: 192.186.0.1:1337��6,   $��Center=�6L   5!      �� 5#   
   $��4��6   $��3��6    $��Assets/FHDW_Logo_RGB.jpg��61   $��	5,15,10,0q�6S   � 5%   
   $��2��6   $��0��6    5&      �� 5'       5)      �� 5*      $
��0,4* �6#   �� 5+      $
��0,6* �6#   .+�5-      �� -   5        $��TimerTextBox��5-   '   $��5,5,5,5q�6    �� AcceptsReturn .  $ �� HorizontalScrollBarVisibility$ Disabled=�6Q    �� TextWrapping$ NoWrap=�6z   $��Center=�6�   $��Stretch=�6�    �� TextAlignment$ Center=�5/   m   ��5.      �� 	 �� Path$	 ActualHeight�
 �� RelativeSource
 ��  �� Mode$ FindAncestor=� �� AncestorType$
 GridL� �� 	Converter# [ 5/   m   (� 50      $��1��6   $��Stretch=�6&   .��51      �� -   5        $��TimerButton��51      $��Timer starten��6,   $��5,5,5,5q�6D   $��Stretch=�6V   6t   52      53      (� 55   
   $��2��6   $��1��6   $��Stretch=�60   .��56      �� $��Einstellungen��6   $��5,5,5,5q�6.   $��Stretch=�6@   57      (� 58   
   $��2��6   $��2��6   $��Stretch=�60   .��59      �� $��Spiel starten��6   $��5,5,5,5q�6.   $��Stretch=�6@   5:      (� 5<   
   $��4��6   $��1��6   $��2��6/   $��Stretch=�6C   .��5=      �� $
��Wrap=�6   ��6,   �� *
TimeString+5>      &   System.Windows.Controls.DataGrid  5@   
   -   5        $��
PlayerView��5@      $��5q�6   $��6��60   $��4��6=      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  }�.    	RowHeight$ 50Q�6�      SelectionMode$ Extended=�6�   ��6�   �� $	 .� �� Mode$ OneWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�   ColumnHeaderStyle 5A   
   �� 5B       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5C      ��  �� Property � �� �� Value$ 18(�61   �� 5D       � ��$ Center=�6C   5E         Columns 5H      4  .System.Windows.Controls.DataGridTemplateColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ Ameisenvolk��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6>      
IsReadOnly .    CellTemplate 5J      �� 5K      .R�5L      (� $��5q�6&   .��5M   "   �� $
��Wrap=�6,   ��6@   �� *Name+5N      5O      5Q        5R      $ Spieler/Team��6)   $ * 6?    .  5S      �� 5T      .R�5U      (� $��5q�6&   .��5V   "   �� $
��Wrap=�6,   ��6@   �� *
PlayerName+5W      5X      5Z        5[      $ Statisch��6)   $ * 6;    .  5\      �� 5]      .R�5^      (� $��15q�6&   .��5_   "   ��  J� IsHitTestVisible .  |�.    d� 	IsChecked  6+   �� *Static+6a   5`      5a      5c        5d      $ Aktualisiert um��6)   $ * 6B    .  5e      �� 5f      .R�5g      (� $��5q�6&   .��5h   "   �� $
��Wrap=�6,   ��6@   �� *LastUpdated+5i      5j      5l      5n      5o      5p      