   M S B A M L   `   `   `  ���� L  HProjet File Rouge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projet_File_Rouge.ViewsProjet_File_Rouge.Views  I)clr-namespace:Projet_File_Rouge.ViewModelProjet_File_Rouge.ViewModel  +    %Projet_File_Rouge.Views.LogCenterView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:Projet_File_Rouge.Views     ?	viewmodel)clr-namespace:Projet_File_Rouge.ViewModel       � 	Resources     left 	 right�� %W   &	        &	 !     �� $��../Img/arrow_left.png���� $��../Img/arrow_right.png��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��Retour��$��Right=�$��Center=�$��12q�$��5q�$��20(����� *NavigateParameterMenuCommand+� $��1��$��Center=� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $��0��$��40��$��40��#��[  
������ 
������ �� $��1��$��12q� �� Padding$ 5q�$��20(�$��Center=����� *
Pagination+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��2��$��40��$��40��#��[ 
������ 
������ � $��2��$��Stretch=� �� $��* ��� $��* ��� $��* ��� $
��Auto ��� $
��Auto �.+�� $��0��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��12q�$ 5q�$��20(�$��Center=�$
��Date��(  "System.Windows.Controls.DatePicker  $��1��$��12q�$��5q�$��20(�$��Center=�   SelectedDate �� *Date+$ PropertyChanged=�� $��1��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��12q�$ 5q�$��20(�$��Center=�$
��Type��� $��1��$��Center=�$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��12q�$ 5q�$��20(�$��Aucune Selection������  �� 
TargetType" ��.���  �� Property � y� �� Value$ Visible=�	 �� Triggers	 �� 
 �� Binding
 �� *TypeListNullVisibility+$ PropertyChanged=� �� Value True.a���  � y�$ 	Collapsed=��� $��1��$��12q�$��5q�$��20(����� *
TypeList+ �� SelectedIndex �� *Type+$ PropertyChanged=�� $��2��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��12q�$ 5q�$��20(�$��Center=�$��Auteur��� $��1��$��Center=�$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��12q�$ 5q�$��20(�$��Aucune Selection������ " ��.���  � y�$ Visible=�	 �� 
 �� *UserListNullVisibility+$ PropertyChanged=� True.a���  � y�$ 	Collapsed=��� $��1��$��12q�$��5q�$��20(����� *
UserList+ �� *UserId+$ PropertyChanged=��� -   $��3��$��12q�$��5q�$��20(�$��Center=�$��Center=�$��Filtrer���� -   $��4��$��12q�$��5q�$��20(�$��Center=�$��Center=�$��Annuler Filtre��&   System.Windows.Controls.DataGrid  $��3��$��12q�$��5q�$��20(�$��Center=�   MinRowHeight$ 40Q�   AutoGenerateColumns$ False��   CanUserResizeColumns$ False��   
IsReadOnly$
 True������ *StackPanelContent+ �� Mode$ OneWay=�$ PropertyChanged=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$
 Date��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *DateFormated+  $ * $ Auteur�� �� *	User.Name+  $ * $ Type de log�� �� *Type+  $ 4* $ Texte�� �� *Text+