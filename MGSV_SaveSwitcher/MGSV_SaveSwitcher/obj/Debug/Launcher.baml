   M S B A M L   `   `   `  ����K  GMGSV_SaveManager, Version=2.5.1.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MGSV_SaveSwitcherMGSV_SaveSwitcher  5   	        MGSV_SaveSwitcher.Launcher   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:MGSV_SaveSwitcher     5   	     � Title$  MGSV Save Manager��5   	   $	��685��6#   $	��450��60    � 
ResizeMode$ CanMinimize=�6<    � Icon$ savemanager.ico��6U    � WindowStartupLocation$ CenterScreen=�6l   .��5	      �� ��5
   
   �� 5       �� ImageSource$ launcher_image.jpg��6    j� Stretch$ UniformToFill=�6:   .+�5   
   � $��0,5,0,0q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��3* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $
��Left=�6(   $��	15 0 10 0q�6C   $��Center=�6V   �� 5      -   5         �Name$ 
GitHubLink��5        �� 	IsEnabled . 	 �� TextDecorations$
	 None{�6C   $2��,https://github.com/thatsafy/MGSV_SaveManager?�6Z   .�6�   .�5      �� -   5        $��GitHubImage��5       $y�Visible=�63   $��GitHub-Mark_White.png��6H   $��30��6g   5      5      �� 5      $��1��6   $*��$Metal Gear Solid V: The Phantom Pain��6(   
�������$��
10 10 10 0q�6g   $��Center=�6{   $$��Tw Cen MT Condensed Extra Bold)�6�   $��16(�6�   �� 5      $��2��6   $��Right=�6(   $��	15 0 10 0q�6D   $��Center=�6W   �� 5      -   5        $ TwitterLink��5        . $
	 None{�6D   $"��https://twitter.com/thatsafy?�6[   .�6�   .�5      �� -   5        $��TwitterImage��5       $y�Visible=�64   $��Twitter.png��6I   $��30��6^   5      5       5!      �� 5#   
   $��Save Manager��6   
�������$��	10 0 10 5q�6;   $��Center=�6N   $$��Tw Cen MT Condensed Extra Bold)�6k   $��24(�6�   � 5%   
   $��10, 350, 125, 5q�6   .+�5&      �� -   5        $��LaunchGameButton��5&   0   $��Launch MGSV: TPP��6   $��0 5 0 5q�6I   $$��Tw Cen MT Condensed Extra Bold)�6[   {�.  $��16(�6�   ���	#FFC10000
���   �
���   �6�   5'      � 5)   
   $��10, 0, 125, 5q�6   .+�5*      �� -   5        $��SavesButton��5*   7   $��Saves��6   $��0 5 0 5q�6%   ���	#FFC10000{�.  $$��Tw Cen MT Condensed Extra Bold)�6�   $��16(�6�   
���   �
���   �6�   5+      � 5-   
   $��10, 0, 125, 5q�6   .+�5.      �� -   5        $��SettingsButton��5.   C   $��Select save first��6   $��0 5 0 5q�61   ���	#FFC10000{�.  $$��Tw Cen MT Condensed Extra Bold)�6�   $��16(�6�   
���   �
���   �6�   5/      � 51   
   $��10, 0, 125, 5q�6   .+�52      �� -   5        $
��Exit��52      $��0 5 0 5q�6$   ���	#FFC10000$$��Tw Cen MT Condensed Extra Bold)�6`   $��16(�6�   
���   �
���   �6�   53      � 55   
   $��10 20 125 5q�6    56      �� 57      $��* �6#   �� 58      $��* �6#   .+�5:      �� -	   5        $��0 0 10 0q�5:      $��0 3 0 3q�6'   $��0��6Q   $��Saves directory��6a   $$��Tw Cen MT Condensed Extra Bold)�6{   $��16(�6�   
���   �
���   ����	#FFC100006�   �� 5;      -
   5        $��0 0 0 0q�5;      $��0 3 0 3q�6&   $��1��6R   $��Configs��6b   $$��Tw Cen MT Condensed Extra Bold)�6t   $��16(�6�   
���   �
���   ����	#FFC100006�   5<      � 5>   
   $��
10 10 10 0q�6    5?      �� 5@      $��* �6#   �� 5A      $��3* �6#   �� 5B      $��3* �6#   .+�5D      �� $��0��6   $
��Left=�6(   $��	15 0 10 0q�6C   $��Center=�6V   �� 5E      -   5        $ 	alertlink��5E        .  $
	 None{�6C   $C��=https://github.com/thatsafy/MGSV_SaveManager/releases/latest/?�6Z   .�6�   .�5F      �� -   5        $��
alertimage��5F       $y�Hidden=�62   $��savemanager.ico��6F   $��30��6_   5H      5I      �� 5J      -   5        $��
UpdateText��5J   ;   $��1��6   $��	10 0 10 0q�6(   $y�Hidden=�6M   $��New version available!��6a   $
��Left=�6   
�������$��12(�6�   $��Center=�6�   �� 5K      -   5        $��Version��5K   ;   $��2��6   $��	10 0 10 0q�6(   $�� ��6J   $��Right=�6R   
�������$��12(�6�   $��Bottom=�6�   5L      5N      5Q      