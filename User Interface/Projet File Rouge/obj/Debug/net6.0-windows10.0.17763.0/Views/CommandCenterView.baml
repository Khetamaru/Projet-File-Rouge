   M S B A M L   `   `   `  ����L  HProjet File Rouge, Version=1.1.4.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projet_File_Rouge.ViewsProjet_File_Rouge.Views  5      /    )Projet_File_Rouge.Views.CommandCenterView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projet_File_Rouge.Views     5        � 	Resources  5
         left 	 right�� 5   
   %s   &	        &	 /     �� 5      $��../Img/arrow_left.png��6!   �� 5      $��../Img/arrow_right.png��6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Retour��6   $��Right=�6/   $��Center=�6K   $��12q�6f   $��5q�6r   $��20(�6~   ��6�   �� *NavigateMainMenuCommand+� 5   
   $��1��6   $��Center=�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5!      �� -   5        $��0��5!      $��40��6%   $��40��60   #��[  6<   
������ 
������ 6�   �� 5#      $��1��6   $��12q�6(    �� Padding$ 5q�64   $��20(�6@   $��Center=�6N   ��6k   �� *
Pagination+ �� UpdateSourceTrigger$ PropertyChanged=��� 5%      -   5        $��2��5%      $��40��6%   $��40��60   #��[ 6<   
������ 
������ 6�   5&      &   System.Windows.Controls.DataGrid  5(   
   -   5        $��2��5(      $��12q�6    $��5q�6,   $��20(�68   $��Center=�6F      MinRowHeight$ 40Q�6c      AutoGenerateColumns$ False��6u      CanUserResizeColumns$
 True��6�      
IsReadOnly$
 True��6�   ��6�   �� *CommandList+	 �� Mode$	 OneWay=�$ PropertyChanged=�
   Columns
 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ Nom de la commande��6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *Name+  5+      $ * 6%   $ Date de livraison prévue��6/    6Q   �� *DeliveryDateFormated+  5,      $ * 6%   $ Status de la commande��6/    6N   �� *State+5.      5/      50      