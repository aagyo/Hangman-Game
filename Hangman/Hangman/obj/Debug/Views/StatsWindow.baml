   M S B A M L   `   `   `  ����B  >Hangman, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Hangman.ViewsHangman.Views  5   	       Hangman.Views.StatsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Hangman.Views     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	    � Title$ StatsWindow��5
   	   $��292.401��6   $��512.335��6.   .��5      &   System.Windows.Controls.DataGrid  -   5        $��UserGrid��5      $	��550��6      AutoGenerateColumns .  $��0,0,0,0q�6r      CanUserResizeRows .     CanUserResizeColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . ���#c2c2a3��6+   �� *SelectedUser.stats+	   ColumnHeaderStyle	 5   
   �� 5      
 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"
  6   .�5      ��  �� Property � �� �� Value$ 25��6+   �� 5       � �� �#7a7a526/   �� 5       � ��$ Center=�6?   5         RowHeaderStyle 5   
   �� 5      :  4System.Windows.Controls.Primitives.DataGridRowHeader"
  6   .�5      ��  � ��$ 25��6+   �� 5       � �� �#c2c2a36/   �� 5       � ��$ Center=�6=   5         RowStyle 5   
   �� 5      )  #System.Windows.Controls.DataGridRow"
  6   .�5      ��  � �� �#c2c2a36/   �� 5       � ��$ Center=�6=   5          Columns 5"   
   0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Category��6!      Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *
Category+6P     5$      $ Total number of games��6!   $	 135 6\    6@   �� *	NrTotal+6\     5%      $
 Wins��6!   $	 135 6I    6/   �� *NrWon+6I     5&      $
 Loss��6!   $	 135 6J    6/   �� *NrLost+6J   5(      5)      