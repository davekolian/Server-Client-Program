   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.ChatWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	   -   5          � Title$  Window1��5   	   $��347.2��6   $��690.4��6(   .��5	      � .+�5
   
   �� -   5         ��Name$ 	txtStatus��5
      $
��Left=�6%   $	��183��6@   $��	16,51,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��483��6�   �� 5   
   -   5        $ 
txtMessage��5      $
��Left=�6&   $��61��6A   $��
16,239,0,0q�6M   $
 Wrap=�6a   $	��Top=�6u   $	��323��6�   6�   �� 5   
   -   5         ��Name$ btnSend��5      $
��Send��6"   $
��Left=�61   $��61��6L   $��344,239,0,0q�6X   $	��Top=�6m   $��80��6�   6�   �� 5   
   -   5        $ txtHost��5      $
��Left=�6#   $��17��6>   $��
100,23,0,0q�6J   $
 Wrap=�6^   $��	127.0.0.1��6r   $	��Top=�6�   $	��126��6�   $��Center=�6�   �� 5   
   -   5        $ txtPort��5      $
��Left=�6#   $��17��6>   $��
289,23,0,0q�6J   $
 Wrap=�6^   $
��1111��6r   $	��Top=�6~   $��58��6�   $��Center=�6�   $ �� HorizontalScrollBarVisibility$
 Auto=�6�   �� 5   
   $��Host IP:��6   $
��Left=�6#   $��27��6>   $��	38,19,0,0q�6J   $	��Top=�6]   $��56��6u    J� RenderTransformOrigin$ 0.299,0.548&�6�   �� 5   
   $��Port:��6   $
��Left=�6    $��27��6;   $��
245,19,0,0q�6G   $	��Top=�6[   $��39��6s   �� 5   
   -   5        $ 
btnConnect��5      $��Connect��6%   $
��Left=�67   $��21��6R   $��
370,21,0,0q�6^   $	��Top=�6r   $��68��6�   6�   �� 5   
   -   5        $��Browse��5      $
��Left=�6"   $��60��6=   $��429,240,0,0q�6I   $	��Top=�6^   $��70��6v   6�   �� 5   
   -	   5        $��listbox��5      $
��Left=�6!   $	��249��6<   $��
514,51,0,0q�6I   $	��Top=�6]   $	��153��6u   �� 5   
   $��List of All Users:��6   $
��Left=�6-   $��27��6H   $��
514,19,0,0q�6T   $	��Top=�6h   $	��153��6�   $ 0.299,0.548&�6�   5      5      