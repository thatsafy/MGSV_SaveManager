   M S B A M L   `   `   `  ���� K  GMGSV_SaveManager, Version=2.5.1.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MGSV_SaveSwitcherMGSV_SaveSwitcher       MGSV_SaveSwitcher.Launcher   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:MGSV_SaveSwitcher       � Title$  MGSV Save Manager��$	��685��$	��450�� � 
ResizeMode$ CanMinimize=� � Icon$ savemanager.ico�� � WindowStartupLocation$ CenterScreen=�.���� ����  �� ImageSource$ launcher_image.jpg�� j� Stretch$ UniformToFill=�.+�� $��0,5,0,0q� � ColumnDefinitions �� $��* ��� $��3* ��� $��* �.+��� $��0��$
��Left=�$��	15 0 10 0q�$��Center=��� -    �Name$ 
GitHubLink�� �� 	IsEnabled . 	 �� TextDecorations$
	 None{�$2��,https://github.com/thatsafy/MGSV_SaveManager?�.�.��� -   $��GitHubImage��$y�Visible=�$��GitHub-Mark_White.png��$��30���� $��1��$*��$Metal Gear Solid V: The Phantom Pain��
�������$��
10 10 10 0q�$��Center=�$$��Tw Cen MT Condensed Extra Bold)�$��16(��� $��2��$��Right=�$��	15 0 10 0q�$��Center=��� -   $ TwitterLink�� . $
	 None{�$"��https://twitter.com/thatsafy?�.�.��� -   $��TwitterImage��$y�Visible=�$��Twitter.png��$��30���� $��Save Manager��
�������$��	10 0 10 5q�$��Center=�$$��Tw Cen MT Condensed Extra Bold)�$��24(�� $��10, 350, 125, 5q�.+��� -   $��LaunchGameButton��$��Launch MGSV: TPP��$��0 5 0 5q�$$��Tw Cen MT Condensed Extra Bold)�{�.  $��16(����	#FFC10000
���   �
���   �� $��10, 0, 125, 5q�.+��� -   $��SavesButton��$��Saves��$��0 5 0 5q����	#FFC10000{�.  $$��Tw Cen MT Condensed Extra Bold)�$��16(�
���   �
���   �� $��10, 0, 125, 5q�.+��� -   $��SettingsButton��$��Select save first��$��0 5 0 5q����	#FFC10000{�.  $$��Tw Cen MT Condensed Extra Bold)�$��16(�
���   �
���   �� $��10, 0, 125, 5q�.+��� -   $
��Exit��$��0 5 0 5q����	#FFC10000$$��Tw Cen MT Condensed Extra Bold)�$��16(�
���   �
���   �� $��10 20 125 5q� �� $��* ��� $��* �.+��� -	   $��0 0 10 0q�$��0 3 0 3q�$��0��$��Saves directory��$$��Tw Cen MT Condensed Extra Bold)�$��16(�
���   �
���   ����	#FFC10000�� -
   $��0 0 0 0q�$��0 3 0 3q�$��1��$��Configs��$$��Tw Cen MT Condensed Extra Bold)�$��16(�
���   �
���   ����	#FFC10000� $��
10 10 10 0q� �� $��* ��� $��3* ��� $��3* �.+��� $��0��$
��Left=�$��	15 0 10 0q�$��Center=��� -   $ 	alertlink�� .  $
	 None{�$C��=https://github.com/thatsafy/MGSV_SaveManager/releases/latest/?�.�.��� -   $��
alertimage��$y�Hidden=�$��savemanager.ico��$��30���� -   $��
UpdateText��$��1��$��	10 0 10 0q�$y�Hidden=�$��New version available!��$
��Left=�
�������$��12(�$��Center=��� -   $��Version��$��2��$��	10 0 10 0q�$�� ��$��Right=�
�������$��12(�$��Bottom=�