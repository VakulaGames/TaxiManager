   M S B A M L   `   `   `  ����E  ATaxiServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TaxiServer
TaxiServer  5   	       TaxiServer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TaxiServer     5   	     � Title$  Такси №1��5	   	   $
�� 600��5   	   $
�� 900��5   	   $y�Visible=�5   	    � 	Resources 5         TaxiServer.BoolConvertor   	Convertor��%   &	          5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $��18 �6   �� 5      $��* �6   �� 5      $��80 �6    � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5    
   �� $��1��6   $��3��5!      $��Stretch=�5"      
������� J� Opacity$ 1Q�5$      $��4��5%      �� 5(   
   -   5         ��Name$ driversList��5(      $��2��5)      $��1��5*       �� View 5,      �� 5-      .P�5.      �� $��Позывной��6%    �� DisplayMemberBinding 67   �� *
Callsign+�� 5/      $��Имя��6%    62   �� *Name+�� 50      $��Авто��6%    63   �� *
Automobile+51      53      �� 5Y   
   $��2��6   $��Водители��5Z      $��Center=�6$   $��Center=�6A   �� 5\   
   -   5        $4��.Добавить нового водителя��5\      $��2��5]      $��
15,55,15,0q�5^      $��2��5_      $	��Top=�5`      6)   �� 5b   
   -   5        $��Изменить��5b      $��2��5c      $
��Left=�5d      $��	15,27,0,0q�5e      $��2��6$   $	��Top=�5f      $	��120��6)   65   �� 5h   
   -   5        $��Удалить��5h      $��2��5i      $
��Left=�5j      $��
140,27,0,0q�5k      $��2��5l      $	��Top=�5m      $	��141��5n      5o      �� 5q   
   -   5         ��Name$ selectedDriver��5q      $��2��5r      $��Center=�5s      $��15,5,0,0q�5t      $��2��5u      $
��Wrap=�5v      $	��Top=�5w      $��17��5x      5{      5|      