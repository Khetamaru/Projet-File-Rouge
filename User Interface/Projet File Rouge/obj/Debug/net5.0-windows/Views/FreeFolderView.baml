   M S B A M L   `   `   `  ����L  HProjet File Rouge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projet_File_Rouge.ViewsProjet_File_Rouge.Views  5      ,    &Projet_File_Rouge.Views.FreeFolderView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projet_File_Rouge.Views     5        � 	Resources  5
         left 	 right�� 5   
   %s   &	        &	 /     �� 5      $��../Img/arrow_left.png��6!   �� 5      $��../Img/arrow_right.png��6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Retour��6   $��Right=�6/   $��Center=�6K   $��12q�6f   $��5q�6r   $��20(�6~   ��6�   �� *NavigateMainMenuCommand+� 5   
   $��1��6   $��Center=�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5"      �� -   5        $��0��5"      $��40��6%   $��40��60   #��[  6<   
������ 
������ 6�   �� 5$      $��1��6   $��12q�6(    �� Padding$ 5q�64   $��20(�6@   $��Center=�6N   ��6k   �� *
Pagination+ �� UpdateSourceTrigger$ PropertyChanged=��� 5&      -   5        $��2��5&      $��40��6%   $��40��60   #��[ 6<   
������ 
������ 6�   5(      � 5*   
   $��2��6   $��Stretch=�6    5+      �� 5,      $��* �6#   �� 5-      $��* �6#   �� 5.      $��* �6#   �� 5/      $
��Auto �6#   �� 50      $
��Auto �6#   .+�53      � $��0��6   $��Center=�6#    54      �� 55      $
��Auto �6$   �� 56      $
��Auto �6$   .+�59      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $
��Date��6l   (  "System.Windows.Controls.DatePicker  5:      $��1��6   $��12q�6*   $��5q�66   $��20(�6B   $��Center=�6P      SelectedDate 6m   �� *Date+$ PropertyChanged=�5;      � 5=      $��1��6   $��Center=�6#    5>      �� 5?      $
��Auto �6$   �� 5@      $
��Auto �6$   .+�5C      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $��
Technicien��6l   � 5D      $��1��6   $��Center=�6$   $��Center=�6?    5E      �� 5F      $
��Auto �6+   �� 5G      $
��Auto �6+   .+�5J      �� $��0��6    $��12q�60   $ 5q�6<   $��20(�6H   $��Aucune Selection��6V   ��5K      �� 5L       �� 
TargetType" ��6$   .�5M   "   ��  �� Property � y� �� Value$ Visible=�6?   	 �� Triggers	 5N   "   �� 5O   &   
 �� Binding
 62   �� *UserListNullVisibility+$ PropertyChanged=� �� Value True6�   .a�5P   *   ��  � y�$ 	Collapsed=�6G   5Q   '   5S      5U      �� 5V      $��1��6   $��12q�6/   $��5q�6;   $��20(�6G   ��6U   �� *
UserList+ �� SelectedIndex 6v   �� *UserId+$ PropertyChanged=�5W      5X      � 5Z      $��2��6   $��Center=�6#    5[      �� 5\      $
��Auto �6$   �� 5]      $
��Auto �6$   .+�5`      �� $��0��6   $��12q�6)   $ 5q�65   $��20(�6A   $��Center=�6O   $��
Nom Client��6l   �� 5a      $��1��6   $��12q�6'   $��5q�63   $��20(�6?   $��Stretch=�6M   ��6k   �� *
ClientName+$ PropertyChanged=�5b      �� 5d      -   5        $��3��5d      $��12q�6%   $��5q�61   $��20(�6=   $��Center=�6K   $��Center=�6h   $��Filtrer��6�   6�   �� 5f      -   5        $��4��5f      $��12q�6%   $��5q�61   $��20(�6=   $��Center=�6K   $��Center=�6h   $��Annuler Filtre��6�   6�   5g      &   System.Windows.Controls.DataGrid  5i   
   -   5        $��3��5i      $��12q�6    $��5q�6,   $��20(�68   $��Center=�6F      MinRowHeight$ 40Q�6c      AutoGenerateColumns$ False��6u      CanUserResizeColumns$ False��6�      
IsReadOnly$
 True��6�   ��6�   �� *RedWireList+ �� Mode$ OneWay=�$ PropertyChanged=�   Columns 5j      0  *System.Windows.Controls.DataGridTextColumn  5k      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ Prise en charge��6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *InChargeDateFormated+  5l      $ * 6%   $ Numéro Prise en Charge��6/    6O   �� *Client+  5m      $ * 6%   $ Type de Support��6/    6H   �� *Type+  5n      $ * 6%   $ Model du Support��6/    6I   �� *Model+  5o      $ * 6%   $ 
Nom Client��6/    6C   �� *
ClientName+5q      5r      5s      