   M S B A M L   `   `   `  ����E  AQuizrunner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Quizrunner
Quizrunner  5   	       Quizrunner.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Quizrunner     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 
QuizRunner��5	   	   $	��760��6   $
��1130��6)   4  .System.Windows.Automation.AutomationProperties   Name$ 
MainWindow��5
   	   .��5      �  � ShowGridLines$ False��6    � ColumnDefinitions 5   
   �� 5      $
��198* �6   �� 5      $
��250* �6   �� 5      $
��330* �6   �� 5      $
��249* �6   �� 5      $
��103* �6   �� 5      $��0* �6    � RowDefinitions 5   
   �� 5      $	��29* �6   6+   �� 5      $	��64* �6   6+    � 	Resources 5   
    �� 
TargetType �� Property	 �� Value��%`   '��       �� 5      " ��6   .�5      ��  �@��Padding$		 155q�6,   6:   5      5      ��5   
   �� 5      
 �� ImageSource$
 /Background.png��6   .+�5!   
   �� -   5         ��Name$ name_project��5!      $��0��6&   $��Franklin Gothic Medium)�63   $��48(�6W   $��10,108,0,52q�6e   $��2��6z   .��6�   Quiz Runner6�   �� 5"   
   -   5        $ help_name_project��5"       J� RenderTransformOrigin$ 0.498,0.449&�6+   $��Calibri)�6O   $��18(�6d   $l��fприложение для создания и
прохождение различных тестов��6r   $��
34,173,0,0q�6�   $��2��6�   �� 5#   
   -   5         ��Name$ Start_button��5#      $��1��6'   $��Bottom=�64   $��73,124,125,117q�6V   $��2��6�      AutomationId$ Start_button��6�   .��5$      �� $��Right=�6   $��Bottom=�65   .+�5%      �� -   5         ��Name$ image��5%      $
��Fill=�6'   $	��144��66   $	��164��6C   $��	35,0,0,25q�6O   $��/quiz_one.png��6b   �� 5&      -   5        $ Title_btn_left��5&      $��Создать тест��60   
����� �$	��246��6Y   $��Center=�6e   $��Center=�6�   $��56��6�    5(      ��%h   '��       �� 5*      " ��6    .�5+      ��  �@��CornerRadius$	 16 16 12 12��6=   5,      5-      5.      50       54      ��%c   '��       �� 56      " ��6   .�57      ��  �@��CornerRadius$	 12��58   	   59      5:       J� Effect 5;      3  -System.Windows.Media.Effects.DropShadowEffect  5<      5?      �� 5@   
   -   5        $ Create_new_game��5@      $��1��6*   $��Bottom=�67   $��10,121,71,120q�6Y   $��2��6p   $ Create_new_game��6�   .��5A      �� .+�5B      �� -   5        $ 	image_one��5B      $	��144��6+   $	��153��68   $��	10,0,0,25q�6D   $
��Fill=�6W   $��/quiz_two.png��6f   �� 5C      -   5        $ Title_btn_right��5C      $+��%Начать тестирование��61   
����� �$	��246��6a   $��Center=�6m   $��Center=�6�   $��56��6�    5D      ��%h   '��       �� 5E      " ��6    .�5F      ��  �@��CornerRadius$	 16 16 12 12��6=   5G      5H      5I      5J       5K      ��%c   '��       �� 5M      " ��6   .�5N      ��  �@��CornerRadius$	 12��5O   	   5P      5Q       5R        5S      5U      �� 5V   
   -	   5        $ Achievements��5V      $��1��6'   $��Bottom=�64   $��52,124,52,117q�6V   $��3��6m   $��2��6�   $ Achievements��6�    5X      ��%c   '��       �� 5Z      " ��6   .�5[      ��  �@��CornerRadius$	 12��5\      5]      5^       5_        5`      .��5b      �� .+�5c      �� -
   5        $ image_three��5c      $	��144��6-   $	��153��6:   $��	10,0,0,25q�6F   $
��Fill=�6Y   $��/icons_dos.png��6h   �� 5d      -   5        $ Title_btn_right1��5d      $��Достижение��62   
����� �$	��246��6Y   $��Center=�6e   $��Center=�6�   $��56��6�    5e      ��%h   '��       �� 5f      " ��6    .�5g      ��  �@��CornerRadius$	 16 16 12 12��6=   5h      5i      5j      5k      5l      5m      5n      