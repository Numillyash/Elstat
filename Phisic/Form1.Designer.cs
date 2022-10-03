namespace Phisic
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.лууToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nullChargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TensityVectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Fill_Multyple = new System.Windows.Forms.ToolStripMenuItem();
            this.deMultiplyLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.blackThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAtomButton = new System.Windows.Forms.Button();
            this.ChargeModuleInput = new System.Windows.Forms.TextBox();
            this.InstructionButton = new System.Windows.Forms.Button();
            this.DeleteSelectedAtomButton = new System.Windows.Forms.Button();
            this.XCoordinateInput = new System.Windows.Forms.TextBox();
            this.YCoordinateInput = new System.Windows.Forms.TextBox();
            this.CreateAtomButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CoordinateGrid = new System.Windows.Forms.CheckBox();
            this.DeselectButton = new System.Windows.Forms.Button();
            this.ShowArrowsCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowRareArrowsCheckBox = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ShowTensityCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ShowE0CheckBox = new System.Windows.Forms.CheckBox();
            this.ConstChargeInputText = new System.Windows.Forms.TextBox();
            this.ConstXInputText = new System.Windows.Forms.TextBox();
            this.ConstYInputText = new System.Windows.Forms.TextBox();
            this.ConstCoordinatesText = new System.Windows.Forms.TextBox();
            this.ConstYOutputText = new System.Windows.Forms.TextBox();
            this.ConstXOutputText = new System.Windows.Forms.TextBox();
            this.YCoordinateOutput = new System.Windows.Forms.TextBox();
            this.XCoordinateOutput = new System.Windows.Forms.TextBox();
            this.ConstChargeText = new System.Windows.Forms.TextBox();
            this.ChargeModuleOutput = new System.Windows.Forms.TextBox();
            this.TensityOutput = new System.Windows.Forms.TextBox();
            this.ConstTensityText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(750, 750);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.лууToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.fillToolStripMenuItem,
            this.lineWidthToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.blackThemeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // лууToolStripMenuItem
            // 
            this.лууToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plusToolStripMenuItem,
            this.minusToolStripMenuItem,
            this.nullChargeToolStripMenuItem,
            this.elcToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.TensityVectorToolStripMenuItem});
            this.лууToolStripMenuItem.Name = "лууToolStripMenuItem";
            this.лууToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.лууToolStripMenuItem.Text = "Новый заряд";
            // 
            // plusToolStripMenuItem
            // 
            this.plusToolStripMenuItem.Name = "plusToolStripMenuItem";
            this.plusToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.plusToolStripMenuItem.Text = "Положительный";
            this.plusToolStripMenuItem.Click += new System.EventHandler(this.plusToolStripMenuItem_Click);
            // 
            // minusToolStripMenuItem
            // 
            this.minusToolStripMenuItem.Name = "minusToolStripMenuItem";
            this.minusToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.minusToolStripMenuItem.Text = "Отрицательный";
            this.minusToolStripMenuItem.Click += new System.EventHandler(this.minusToolStripMenuItem_Click);
            // 
            // nullChargeToolStripMenuItem
            // 
            this.nullChargeToolStripMenuItem.Name = "nullChargeToolStripMenuItem";
            this.nullChargeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nullChargeToolStripMenuItem.Text = "Нейтральный";
            this.nullChargeToolStripMenuItem.Click += new System.EventHandler(this.nullChargeToolStripMenuItem_Click);
            // 
            // elcToolStripMenuItem
            // 
            this.elcToolStripMenuItem.Name = "elcToolStripMenuItem";
            this.elcToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.elcToolStripMenuItem.Text = "Пробный";
            this.elcToolStripMenuItem.Visible = false;
            this.elcToolStripMenuItem.Click += new System.EventHandler(this.elcToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveToolStripMenuItem.Text = "Изменение";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // TensityVectorToolStripMenuItem
            // 
            this.TensityVectorToolStripMenuItem.Name = "TensityVectorToolStripMenuItem";
            this.TensityVectorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TensityVectorToolStripMenuItem.Text = "Вектор поля";
            this.TensityVectorToolStripMenuItem.Click += new System.EventHandler(this.TensityVectorToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.clearToolStripMenuItem.Text = "Очистка поля";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // fillToolStripMenuItem
            // 
            this.fillToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Fill_Multyple,
            this.deMultiplyLinesToolStripMenuItem,
            this.clearFillToolStripMenuItem});
            this.fillToolStripMenuItem.Name = "fillToolStripMenuItem";
            this.fillToolStripMenuItem.Size = new System.Drawing.Size(163, 20);
            this.fillToolStripMenuItem.Text = "Увеличение кол-ва линий";
            // 
            // Fill_Multyple
            // 
            this.Fill_Multyple.Name = "Fill_Multyple";
            this.Fill_Multyple.Size = new System.Drawing.Size(235, 22);
            this.Fill_Multyple.Text = "Увеличить в 2 раза";
            this.Fill_Multyple.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // deMultiplyLinesToolStripMenuItem
            // 
            this.deMultiplyLinesToolStripMenuItem.Name = "deMultiplyLinesToolStripMenuItem";
            this.deMultiplyLinesToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deMultiplyLinesToolStripMenuItem.Text = "Убрать половину";
            this.deMultiplyLinesToolStripMenuItem.Click += new System.EventHandler(this.deMultiplyLinesToolStripMenuItem_Click);
            // 
            // clearFillToolStripMenuItem
            // 
            this.clearFillToolStripMenuItem.Name = "clearFillToolStripMenuItem";
            this.clearFillToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.clearFillToolStripMenuItem.Text = "Очистить добавочные линии";
            this.clearFillToolStripMenuItem.Click += new System.EventHandler(this.clearFillToolStripMenuItem_Click);
            // 
            // lineWidthToolStripMenuItem
            // 
            this.lineWidthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.lineWidthToolStripMenuItem.Name = "lineWidthToolStripMenuItem";
            this.lineWidthToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.lineWidthToolStripMenuItem.Text = "Толщина линий";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "1";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem7.Text = "2";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem8.Text = "3";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem9.Text = "4";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem10.Text = "5";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem2,
            this.saveAsToolStripMenuItem1});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.saveToolStripMenuItem.Text = "Сохранение";
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.saveToolStripMenuItem2.Text = "Сохранить по-умолчанию";
            this.saveToolStripMenuItem2.Click += new System.EventHandler(this.saveToolStripMenuItem2_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.saveAsToolStripMenuItem1.Text = "Сохранить как";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem2,
            this.loadAsToolStripMenuItem1});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.loadToolStripMenuItem.Text = "Загрузка";
            // 
            // loadToolStripMenuItem2
            // 
            this.loadToolStripMenuItem2.Name = "loadToolStripMenuItem2";
            this.loadToolStripMenuItem2.Size = new System.Drawing.Size(216, 22);
            this.loadToolStripMenuItem2.Text = "Загрузить по-умолчанию";
            this.loadToolStripMenuItem2.Click += new System.EventHandler(this.loadToolStripMenuItem2_Click);
            // 
            // loadAsToolStripMenuItem1
            // 
            this.loadAsToolStripMenuItem1.Name = "loadAsToolStripMenuItem1";
            this.loadAsToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.loadAsToolStripMenuItem1.Text = "Загрузить как";
            this.loadAsToolStripMenuItem1.Click += new System.EventHandler(this.loadAsToolStripMenuItem1_Click);
            // 
            // blackThemeToolStripMenuItem
            // 
            this.blackThemeToolStripMenuItem.Name = "blackThemeToolStripMenuItem";
            this.blackThemeToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.blackThemeToolStripMenuItem.Text = "Dark Theme";
            this.blackThemeToolStripMenuItem.Visible = false;
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // loadAsToolStripMenuItem
            // 
            this.loadAsToolStripMenuItem.Name = "loadAsToolStripMenuItem";
            this.loadAsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // DeleteAtomButton
            // 
            this.DeleteAtomButton.Location = new System.Drawing.Point(770, 31);
            this.DeleteAtomButton.Name = "DeleteAtomButton";
            this.DeleteAtomButton.Size = new System.Drawing.Size(112, 50);
            this.DeleteAtomButton.TabIndex = 7;
            this.DeleteAtomButton.Text = "Удалить тестовый заряд";
            this.DeleteAtomButton.UseVisualStyleBackColor = true;
            this.DeleteAtomButton.Click += new System.EventHandler(this.DeleteAtomButton_Click);
            // 
            // ChargeModuleInput
            // 
            this.ChargeModuleInput.Location = new System.Drawing.Point(770, 169);
            this.ChargeModuleInput.Name = "ChargeModuleInput";
            this.ChargeModuleInput.Size = new System.Drawing.Size(112, 20);
            this.ChargeModuleInput.TabIndex = 8;
            this.ChargeModuleInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ChargeModuleInput.TextChanged += new System.EventHandler(this.ChargeModuleInput_TextChanged);
            // 
            // InstructionButton
            // 
            this.InstructionButton.Location = new System.Drawing.Point(770, 730);
            this.InstructionButton.Name = "InstructionButton";
            this.InstructionButton.Size = new System.Drawing.Size(112, 50);
            this.InstructionButton.TabIndex = 9;
            this.InstructionButton.Text = "Инструкции";
            this.InstructionButton.UseVisualStyleBackColor = true;
            this.InstructionButton.Click += new System.EventHandler(this.InstructionButton_Click);
            // 
            // DeleteSelectedAtomButton
            // 
            this.DeleteSelectedAtomButton.Location = new System.Drawing.Point(770, 87);
            this.DeleteSelectedAtomButton.Name = "DeleteSelectedAtomButton";
            this.DeleteSelectedAtomButton.Size = new System.Drawing.Size(112, 50);
            this.DeleteSelectedAtomButton.TabIndex = 10;
            this.DeleteSelectedAtomButton.Text = "Удалить выбранный заряд";
            this.DeleteSelectedAtomButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedAtomButton.Click += new System.EventHandler(this.DeleteSelectedAtomButton_Click);
            // 
            // XCoordinateInput
            // 
            this.XCoordinateInput.Location = new System.Drawing.Point(770, 221);
            this.XCoordinateInput.Name = "XCoordinateInput";
            this.XCoordinateInput.Size = new System.Drawing.Size(45, 20);
            this.XCoordinateInput.TabIndex = 13;
            this.XCoordinateInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.XCoordinateInput.TextChanged += new System.EventHandler(this.XCoordinateInput_TextChanged);
            // 
            // YCoordinateInput
            // 
            this.YCoordinateInput.Location = new System.Drawing.Point(837, 221);
            this.YCoordinateInput.Name = "YCoordinateInput";
            this.YCoordinateInput.Size = new System.Drawing.Size(45, 20);
            this.YCoordinateInput.TabIndex = 14;
            this.YCoordinateInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YCoordinateInput.TextChanged += new System.EventHandler(this.YCoordinateInput_TextChanged);
            // 
            // CreateAtomButton
            // 
            this.CreateAtomButton.Location = new System.Drawing.Point(770, 247);
            this.CreateAtomButton.Name = "CreateAtomButton";
            this.CreateAtomButton.Size = new System.Drawing.Size(112, 50);
            this.CreateAtomButton.TabIndex = 16;
            this.CreateAtomButton.Text = "Создать заряд";
            this.CreateAtomButton.UseVisualStyleBackColor = true;
            this.CreateAtomButton.Click += new System.EventHandler(this.CreateAtomButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(777, 567);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 17;
            // 
            // CoordinateGrid
            // 
            this.CoordinateGrid.Location = new System.Drawing.Point(770, 303);
            this.CoordinateGrid.Name = "CoordinateGrid";
            this.CoordinateGrid.Size = new System.Drawing.Size(112, 33);
            this.CoordinateGrid.TabIndex = 18;
            this.CoordinateGrid.Text = "Координатная сетка";
            this.CoordinateGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CoordinateGrid.UseVisualStyleBackColor = true;
            this.CoordinateGrid.CheckedChanged += new System.EventHandler(this.CoordinateGrid_CheckedChanged);
            // 
            // DeselectButton
            // 
            this.DeselectButton.Location = new System.Drawing.Point(770, 674);
            this.DeselectButton.Name = "DeselectButton";
            this.DeselectButton.Size = new System.Drawing.Size(112, 50);
            this.DeselectButton.TabIndex = 23;
            this.DeselectButton.Text = "Снять выделение";
            this.DeselectButton.UseVisualStyleBackColor = true;
            this.DeselectButton.Click += new System.EventHandler(this.DeselectButton_Click);
            // 
            // ShowArrowsCheckBox
            // 
            this.ShowArrowsCheckBox.Location = new System.Drawing.Point(770, 648);
            this.ShowArrowsCheckBox.Name = "ShowArrowsCheckBox";
            this.ShowArrowsCheckBox.Size = new System.Drawing.Size(112, 20);
            this.ShowArrowsCheckBox.TabIndex = 24;
            this.ShowArrowsCheckBox.Text = "Стрелки";
            this.ShowArrowsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ShowArrowsCheckBox.UseVisualStyleBackColor = true;
            this.ShowArrowsCheckBox.CheckedChanged += new System.EventHandler(this.ShowArrowsCheckBox_CheckedChanged);
            // 
            // ShowRareArrowsCheckBox
            // 
            this.ShowRareArrowsCheckBox.Location = new System.Drawing.Point(770, 622);
            this.ShowRareArrowsCheckBox.Name = "ShowRareArrowsCheckBox";
            this.ShowRareArrowsCheckBox.Size = new System.Drawing.Size(112, 20);
            this.ShowRareArrowsCheckBox.TabIndex = 25;
            this.ShowRareArrowsCheckBox.Text = "Редкие стрелки";
            this.ShowRareArrowsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ShowRareArrowsCheckBox.UseVisualStyleBackColor = true;
            this.ShowRareArrowsCheckBox.CheckedChanged += new System.EventHandler(this.ShowRareArrowsCheckBox_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(797, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(54, 13);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "georgul.ru";
            // 
            // ShowTensityCheckBox
            // 
            this.ShowTensityCheckBox.Location = new System.Drawing.Point(770, 596);
            this.ShowTensityCheckBox.Name = "ShowTensityCheckBox";
            this.ShowTensityCheckBox.Size = new System.Drawing.Size(112, 20);
            this.ShowTensityCheckBox.TabIndex = 27;
            this.ShowTensityCheckBox.Text = "Напр. поля";
            this.ShowTensityCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ShowTensityCheckBox.UseVisualStyleBackColor = true;
            this.ShowTensityCheckBox.CheckedChanged += new System.EventHandler(this.ShowTensityCheckBox_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ShowE0CheckBox
            // 
            this.ShowE0CheckBox.Location = new System.Drawing.Point(770, 570);
            this.ShowE0CheckBox.Name = "ShowE0CheckBox";
            this.ShowE0CheckBox.Size = new System.Drawing.Size(112, 20);
            this.ShowE0CheckBox.TabIndex = 28;
            this.ShowE0CheckBox.Text = "Show E = 0";
            this.ShowE0CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ShowE0CheckBox.UseVisualStyleBackColor = true;
            this.ShowE0CheckBox.CheckedChanged += new System.EventHandler(this.ShowE0CheckBox_CheckedChanged);
            // 
            // ConstChargeInputText
            // 
            this.ConstChargeInputText.Location = new System.Drawing.Point(770, 143);
            this.ConstChargeInputText.Name = "ConstChargeInputText";
            this.ConstChargeInputText.ReadOnly = true;
            this.ConstChargeInputText.Size = new System.Drawing.Size(112, 20);
            this.ConstChargeInputText.TabIndex = 31;
            this.ConstChargeInputText.Text = "Модуль заряда";
            this.ConstChargeInputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstXInputText
            // 
            this.ConstXInputText.Location = new System.Drawing.Point(770, 195);
            this.ConstXInputText.Name = "ConstXInputText";
            this.ConstXInputText.ReadOnly = true;
            this.ConstXInputText.Size = new System.Drawing.Size(45, 20);
            this.ConstXInputText.TabIndex = 32;
            this.ConstXInputText.Text = "x";
            this.ConstXInputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstYInputText
            // 
            this.ConstYInputText.Location = new System.Drawing.Point(837, 195);
            this.ConstYInputText.Name = "ConstYInputText";
            this.ConstYInputText.ReadOnly = true;
            this.ConstYInputText.Size = new System.Drawing.Size(45, 20);
            this.ConstYInputText.TabIndex = 33;
            this.ConstYInputText.Text = "y";
            this.ConstYInputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstCoordinatesText
            // 
            this.ConstCoordinatesText.Location = new System.Drawing.Point(770, 337);
            this.ConstCoordinatesText.Name = "ConstCoordinatesText";
            this.ConstCoordinatesText.ReadOnly = true;
            this.ConstCoordinatesText.Size = new System.Drawing.Size(112, 20);
            this.ConstCoordinatesText.TabIndex = 34;
            this.ConstCoordinatesText.Text = "Координаты";
            this.ConstCoordinatesText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstYOutputText
            // 
            this.ConstYOutputText.Location = new System.Drawing.Point(837, 363);
            this.ConstYOutputText.Name = "ConstYOutputText";
            this.ConstYOutputText.ReadOnly = true;
            this.ConstYOutputText.Size = new System.Drawing.Size(45, 20);
            this.ConstYOutputText.TabIndex = 36;
            this.ConstYOutputText.Text = "y";
            this.ConstYOutputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstXOutputText
            // 
            this.ConstXOutputText.Location = new System.Drawing.Point(770, 363);
            this.ConstXOutputText.Name = "ConstXOutputText";
            this.ConstXOutputText.ReadOnly = true;
            this.ConstXOutputText.Size = new System.Drawing.Size(45, 20);
            this.ConstXOutputText.TabIndex = 35;
            this.ConstXOutputText.Text = "x";
            this.ConstXOutputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // YCoordinateOutput
            // 
            this.YCoordinateOutput.Location = new System.Drawing.Point(837, 389);
            this.YCoordinateOutput.Name = "YCoordinateOutput";
            this.YCoordinateOutput.ReadOnly = true;
            this.YCoordinateOutput.Size = new System.Drawing.Size(45, 20);
            this.YCoordinateOutput.TabIndex = 38;
            this.YCoordinateOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // XCoordinateOutput
            // 
            this.XCoordinateOutput.Location = new System.Drawing.Point(770, 389);
            this.XCoordinateOutput.Name = "XCoordinateOutput";
            this.XCoordinateOutput.ReadOnly = true;
            this.XCoordinateOutput.Size = new System.Drawing.Size(45, 20);
            this.XCoordinateOutput.TabIndex = 37;
            this.XCoordinateOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstChargeText
            // 
            this.ConstChargeText.Location = new System.Drawing.Point(770, 415);
            this.ConstChargeText.Name = "ConstChargeText";
            this.ConstChargeText.ReadOnly = true;
            this.ConstChargeText.Size = new System.Drawing.Size(112, 20);
            this.ConstChargeText.TabIndex = 39;
            this.ConstChargeText.Text = "Модуль заряда (нКл)";
            this.ConstChargeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ChargeModuleOutput
            // 
            this.ChargeModuleOutput.Location = new System.Drawing.Point(770, 441);
            this.ChargeModuleOutput.Name = "ChargeModuleOutput";
            this.ChargeModuleOutput.ReadOnly = true;
            this.ChargeModuleOutput.Size = new System.Drawing.Size(112, 20);
            this.ChargeModuleOutput.TabIndex = 40;
            this.ChargeModuleOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TensityOutput
            // 
            this.TensityOutput.Location = new System.Drawing.Point(770, 506);
            this.TensityOutput.Name = "TensityOutput";
            this.TensityOutput.ReadOnly = true;
            this.TensityOutput.Size = new System.Drawing.Size(112, 20);
            this.TensityOutput.TabIndex = 42;
            this.TensityOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConstTensityText
            // 
            this.ConstTensityText.Location = new System.Drawing.Point(770, 467);
            this.ConstTensityText.Multiline = true;
            this.ConstTensityText.Name = "ConstTensityText";
            this.ConstTensityText.ReadOnly = true;
            this.ConstTensityText.Size = new System.Drawing.Size(112, 33);
            this.ConstTensityText.TabIndex = 41;
            this.ConstTensityText.Text = "Напряженность\r\n(В/мм)\r\n";
            this.ConstTensityText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(894, 791);
            this.Controls.Add(this.TensityOutput);
            this.Controls.Add(this.ConstTensityText);
            this.Controls.Add(this.ChargeModuleOutput);
            this.Controls.Add(this.ConstChargeText);
            this.Controls.Add(this.YCoordinateOutput);
            this.Controls.Add(this.XCoordinateOutput);
            this.Controls.Add(this.ConstYOutputText);
            this.Controls.Add(this.ConstXOutputText);
            this.Controls.Add(this.ConstCoordinatesText);
            this.Controls.Add(this.ConstYInputText);
            this.Controls.Add(this.ConstXInputText);
            this.Controls.Add(this.ConstChargeInputText);
            this.Controls.Add(this.ShowE0CheckBox);
            this.Controls.Add(this.ShowTensityCheckBox);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ShowRareArrowsCheckBox);
            this.Controls.Add(this.ShowArrowsCheckBox);
            this.Controls.Add(this.DeselectButton);
            this.Controls.Add(this.CoordinateGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CreateAtomButton);
            this.Controls.Add(this.YCoordinateInput);
            this.Controls.Add(this.XCoordinateInput);
            this.Controls.Add(this.DeleteSelectedAtomButton);
            this.Controls.Add(this.InstructionButton);
            this.Controls.Add(this.ChargeModuleInput);
            this.Controls.Add(this.DeleteAtomButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(910, 830);
            this.MinimumSize = new System.Drawing.Size(910, 830);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Симуляция силовых линий электростатического поля v3.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem лууToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Fill_Multyple;
        private System.Windows.Forms.Button DeleteAtomButton;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.TextBox ChargeModuleInput;
        private System.Windows.Forms.Button InstructionButton;
        private System.Windows.Forms.ToolStripMenuItem nullChargeToolStripMenuItem;
        private System.Windows.Forms.Button DeleteSelectedAtomButton;
        private System.Windows.Forms.TextBox XCoordinateInput;
        private System.Windows.Forms.TextBox YCoordinateInput;
        private System.Windows.Forms.Button CreateAtomButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CoordinateGrid;
        private System.Windows.Forms.Button DeselectButton;
        private System.Windows.Forms.CheckBox ShowArrowsCheckBox;
        private System.Windows.Forms.ToolStripMenuItem lineWidthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.CheckBox ShowRareArrowsCheckBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolStripMenuItem blackThemeToolStripMenuItem;
        private System.Windows.Forms.CheckBox ShowTensityCheckBox;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem loadAsToolStripMenuItem1;
        private System.Windows.Forms.CheckBox ShowE0CheckBox;
        private System.Windows.Forms.ToolStripMenuItem clearFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deMultiplyLinesToolStripMenuItem;
        private System.Windows.Forms.TextBox ConstChargeInputText;
        private System.Windows.Forms.TextBox ConstXInputText;
        private System.Windows.Forms.TextBox ConstYInputText;
        private System.Windows.Forms.TextBox ConstCoordinatesText;
        private System.Windows.Forms.TextBox ConstYOutputText;
        private System.Windows.Forms.TextBox ConstXOutputText;
        private System.Windows.Forms.TextBox YCoordinateOutput;
        private System.Windows.Forms.TextBox XCoordinateOutput;
        private System.Windows.Forms.TextBox ConstChargeText;
        private System.Windows.Forms.TextBox ChargeModuleOutput;
        private System.Windows.Forms.ToolStripMenuItem TensityVectorToolStripMenuItem;
        private System.Windows.Forms.TextBox TensityOutput;
        private System.Windows.Forms.TextBox ConstTensityText;
    }
}

