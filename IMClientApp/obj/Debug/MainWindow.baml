   M S B A M L   `   `   `  ����F  BIMClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IMClientAppIMClientApp  5   	       IMClientApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:IMClientApp     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      $
��Auto �6   6.    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   �� 5      $
��Auto �6   6,   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��UserFirstName��5      $��14(�6U   $��5q�6c   ��6-   �� *LoggedInUser.FirstName+6c   �� 5      -   5        $��UserLastName��5      $��14(�6S   $��5q�6a   ��6,   �� *LoggedInUser.LastName+6a   5      �� 5   
   -   5        $��LogoutButton��5      $��Logout��6%   $��2��66   $��0��6F   $��1��6S   $��5q�6g   6r   �� 5   
   -   5        $��Contacts��5      $��0��6"   $��1��62   $��2��6?   ��6P   �� *ContactsDetails+��5      �� 5      .R�5      �  5      �� 5      61   �� 5      61   �� 5       61    5"      �� 5#      �� 5$      .+�5&      �� $��	FirstName��6$   $��0��65   $��0��6E   $��5q�6m   ��6R   �� *	FirstName+6m   �� 5'      $��LastName��6$   $��1��64   $��0��6D   $��5q�6k   ��6Q   �� *
LastName+6k   �� 5(      $��LastMessage��6$   $��0��67   $��1��6G   $��2��6T   $��5q�6�   ��6h   �� *LastMessageContent+6�   �� 5)      $��LastMessageTime��6$   $��2��6;   $��2��6K   $��5q�6y   $
��Wrap=�6�   ��6X   �� *LastMessageTime+6�   5*      5+      5.      �� 50   
   -   5        $��Conversation��50      $��1��6'   $��1��67   $��2��6D   $��1��6X   ��6i   �� *MessagesCollection+��51      �� 52      .R�53      �  54      �� 55       57      �� 58      �� 59      .+�5;      �� $��MessageContent��6    $��0��66   $��0��6F   ��6S   �� *	Content+�� 5<      $��MessageTime��6    $��0��63   $��1��6C   ��6P   �� *
TimeSent+5=      5>      5@      � 5A   
   $��0��6   $��2��6    5B      �� 5C      �� 5D       5F      �� 5G      .+�5I      �� -   5        $��AddContactButton��5I      $��0��6-   $��0��6=   $��Add New Contact��6J   $��5q�6�   �� 5J      -   5        $��DeleteContactButton��5J      $��1��60   $��0��6@   $��Delete Contact��6M   $��5q�6�   5K      �� 5M   
   -   5        $��NewMessageTextBox��5M      $��1��6+   $��2��6;    �� TextWrapping$
 Wrap=�6H   ��6\   �� *MessageContent+�� 5N   
   -	   5        $��
SendButton��5N      $
��Send��6#   $��2��62   $��2��6B   $��5q�6h   5O      5P      