   M S B A M L   `   `   `  ����F  BIMClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IMClientAppIMClientApp  5   	       IMClientApp.SignInWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:IMClientApp     5   	   -   5          � Title$  SignInWindow��5	   	   $��430.192��6   $��708.363��6/   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$   $��2��61   $F��@Welcome to the Instant Messaging App! Please log in or register:��6E   $
��Wrap=�6�   $��16(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $��0��6   $��1��6$   $��Username��61   $��14(�6A   $��Center=�6O   $��5q�6j   �� 5   
   -   5        $��SignInUsernameTextBox��5      $��0��6/   $��2��6?    �� TextWrapping$ NoWrap=�6L    �� AcceptsReturn .  $��5q�6x   ��6�   �� *SignInUsername+�� 5    
   $��0��6   $��3��6$   $��Password��61   $��14(�6A   $��Center=�6O   $��5q�6j   �� 5!   
   -   5        $��PasswordTextBox��5!      $��0��6)   $��4��69   $ NoWrap=�6F    .  $��5q�6r   ��6}   �� *SignInPassword+�� 5"   
   -   5        $��SignInButton��5"      $��0��6%   $��5��65   $��Sign in��6B   $��5q�6T   6_   �� 5$   
   $��1��6   $��1��6$   $��Username��61   $��14(�6A   $��Center=�6O   $��5q�6j   �� 5%   
   -   5        $��RegisterUsernameTextBox��5%      $��1��61   $��2��6A   $ NoWrap=�6N    .  $��5q�6z   ��6�   �� *RegisterUsername+�� 5&   
   $��1��6   $��3��6$   $��
First name��61   $��14(�6C   $��Center=�6Q   $��5q�6l   �� 5'   
   -   5        $��FirstNameTextBox��5'      $��1��6*   $��4��6:   $ NoWrap=�6G    .  $��5q�6s   ��6~   �� *	FirstName+�� 5(   
   $��1��6   $��5��6$   $��	Last name��61   $��14(�6B   $��Center=�6P   $��5q�6k   �� 5)   
   -   5        $��LastNameTextBox��5)      $��1��6)   $��6��69   $ NoWrap=�6F    .  $��5q�6r   ��6}   �� *
LastName+�� 5*   
   $��1��6   $��7��6$   $��Password��61   $��14(�6A   $��Center=�6O   $��5q�6j   �� 5+   
   -   5        $��RegisterPasswordTextBox��5+      $��1��61   $��8��6A   $ NoWrap=�6N    .  $��5q�6z   ��6�   �� *RegisterPassword+�� 5,   
   $��1��6   $��9��6$   $��Confirm password��61   $��14(�6I   $��Center=�6W   $��5q�6r   �� 5-   
   -	   5        $��ConfirmPasswordTextBox��5-      $��1��60   $��10��6@   $ NoWrap=�6N    .  $��5q�6z   ��6�   �� *ConfirmPassword+�� 5.   
   -
   5        $��RegisterButton��5.      $��1��6'   $��11��67   $��Register��6E   $��5q�6X   6c   5/      50      