#pragma once
#include "aCandlestick.h"
#include "candlestickUtilities.h"

namespace CppCLRWinFormsProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Summary for FormMain
	/// </summary>
	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}
		// new constructor, pass the individual file path, the start date, and the end date to the new form
		FormMain(String^ filePath, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			// read candlestick data from the CSV file
			candlestickList = candlestickUtilities::readCandlesticksFromCsv(filePath);

			// filter the candlestick data based on the selected date range
			System::Collections::Generic::List<aCandlestick^>^ filteredData = candlestickUtilities::filterCandlestickData(candlestickList, startDate, endDate);
			// convert the old filtered data to the new smart candlestick
			System::Collections::Generic::List<aSmartCandlestick^>^ smartFilteredData = candlestickUtilities::buildSmartCandlesticks(filteredData);
			// send the updated list to the DataGridView for display
			canldestickDataGridView->DataSource = smartFilteredData;

			// Set the X & Y values of the new dot series to show peaks and valleys. 
			chartStockData->Series["Dots"]->XValueMember = "date";
			chartStockData->Series["Dots"]->YValueMembers = "peakValue";
			// Set the X & Y values of the valley series.
			chartStockData->Series["Valleys"]->XValueMember = "date";
			chartStockData->Series["Valleys"]->YValueMembers = "valleyValue";

			// Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
			chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aSmartCandlestick^>(smartFilteredData);
			chartStockData->DataBind();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}

	private: System::Windows::Forms::OpenFileDialog^ openFileDialogSelectFile;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerStartDate;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerEndDate;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::DataGridView^ canldestickDataGridView;
	private: System::Windows::Forms::Button^ redisplayButton;
	// Lists to hold candlestick data
	private: List<aCandlestick^>^ candlestickList = gcnew List<aCandlestick^>();
	private: List<aCandlestick^>^ filteredCandlestickList = gcnew List<aCandlestick^>();
	private: System::Windows::Forms::DataVisualization::Charting::Chart^ chartStockData;





	protected:


	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			System::Windows::Forms::DataGridViewCellStyle^ dataGridViewCellStyle1 = (gcnew System::Windows::Forms::DataGridViewCellStyle());
			System::Windows::Forms::DataGridViewCellStyle^ dataGridViewCellStyle2 = (gcnew System::Windows::Forms::DataGridViewCellStyle());
			System::Windows::Forms::DataGridViewCellStyle^ dataGridViewCellStyle3 = (gcnew System::Windows::Forms::DataGridViewCellStyle());
			System::Windows::Forms::DataVisualization::Charting::ChartArea^ chartArea1 = (gcnew System::Windows::Forms::DataVisualization::Charting::ChartArea());
			System::Windows::Forms::DataVisualization::Charting::ChartArea^ chartArea2 = (gcnew System::Windows::Forms::DataVisualization::Charting::ChartArea());
			System::Windows::Forms::DataVisualization::Charting::Legend^ legend1 = (gcnew System::Windows::Forms::DataVisualization::Charting::Legend());
			System::Windows::Forms::DataVisualization::Charting::Series^ series1 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Series^ series2 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Series^ series3 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Series^ series4 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			this->openFileDialogSelectFile = (gcnew System::Windows::Forms::OpenFileDialog());
			this->dateTimePickerStartDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->dateTimePickerEndDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->canldestickDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->redisplayButton = (gcnew System::Windows::Forms::Button());
			this->chartStockData = (gcnew System::Windows::Forms::DataVisualization::Charting::Chart());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->canldestickDataGridView))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->chartStockData))->BeginInit();
			this->SuspendLayout();
			// 
			// openFileDialogSelectFile
			// 
			this->openFileDialogSelectFile->DefaultExt = L"csv";
			this->openFileDialogSelectFile->FileName = L"NVDA_Daily.csv";
			this->openFileDialogSelectFile->Filter = L"All files (*.*)|*.*| Daily | *_daily.csv*| Weekly  | *_weekly.csv| Montly | *_mon"
				L"thly.csv";
			this->openFileDialogSelectFile->InitialDirectory = L"..\\..\\..\\..\\Stock Data";
			this->openFileDialogSelectFile->Multiselect = true;
			this->openFileDialogSelectFile->RestoreDirectory = true;
			this->openFileDialogSelectFile->FileOk += gcnew System::ComponentModel::CancelEventHandler(this, &FormMain::openFileDialog1_FileOk);
			// 
			// dateTimePickerStartDate
			// 
			this->dateTimePickerStartDate->Location = System::Drawing::Point(534, 501);
			this->dateTimePickerStartDate->Name = L"dateTimePickerStartDate";
			this->dateTimePickerStartDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerStartDate->TabIndex = 1;
			this->dateTimePickerStartDate->Value = System::DateTime(2026, 6, 1, 0, 0, 0, 0);
			// 
			// dateTimePickerEndDate
			// 
			this->dateTimePickerEndDate->Location = System::Drawing::Point(534, 615);
			this->dateTimePickerEndDate->Name = L"dateTimePickerEndDate";
			this->dateTimePickerEndDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerEndDate->TabIndex = 2;
			this->dateTimePickerEndDate->Value = System::DateTime(2026, 9, 18, 0, 0, 0, 0);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(607, 475);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(55, 13);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Start Date";
			this->label1->Click += gcnew System::EventHandler(this, &FormMain::label1_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(607, 591);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(52, 13);
			this->label2->TabIndex = 4;
			this->label2->Text = L"End Date";
			// 
			// canldestickDataGridView
			// 
			dataGridViewCellStyle1->Alignment = System::Windows::Forms::DataGridViewContentAlignment::MiddleLeft;
			dataGridViewCellStyle1->BackColor = System::Drawing::SystemColors::Control;
			dataGridViewCellStyle1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Regular,
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			dataGridViewCellStyle1->ForeColor = System::Drawing::SystemColors::WindowText;
			dataGridViewCellStyle1->SelectionBackColor = System::Drawing::SystemColors::Highlight;
			dataGridViewCellStyle1->SelectionForeColor = System::Drawing::SystemColors::HighlightText;
			dataGridViewCellStyle1->WrapMode = System::Windows::Forms::DataGridViewTriState::True;
			this->canldestickDataGridView->ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this->canldestickDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			dataGridViewCellStyle2->Alignment = System::Windows::Forms::DataGridViewContentAlignment::MiddleLeft;
			dataGridViewCellStyle2->BackColor = System::Drawing::SystemColors::Window;
			dataGridViewCellStyle2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Regular,
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			dataGridViewCellStyle2->ForeColor = System::Drawing::SystemColors::ControlText;
			dataGridViewCellStyle2->SelectionBackColor = System::Drawing::SystemColors::Highlight;
			dataGridViewCellStyle2->SelectionForeColor = System::Drawing::SystemColors::HighlightText;
			dataGridViewCellStyle2->WrapMode = System::Windows::Forms::DataGridViewTriState::False;
			this->canldestickDataGridView->DefaultCellStyle = dataGridViewCellStyle2;
			this->canldestickDataGridView->Location = System::Drawing::Point(12, 458);
			this->canldestickDataGridView->Name = L"canldestickDataGridView";
			dataGridViewCellStyle3->Alignment = System::Windows::Forms::DataGridViewContentAlignment::MiddleLeft;
			dataGridViewCellStyle3->BackColor = System::Drawing::SystemColors::Control;
			dataGridViewCellStyle3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Regular,
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			dataGridViewCellStyle3->ForeColor = System::Drawing::SystemColors::WindowText;
			dataGridViewCellStyle3->SelectionBackColor = System::Drawing::SystemColors::Highlight;
			dataGridViewCellStyle3->SelectionForeColor = System::Drawing::SystemColors::HighlightText;
			dataGridViewCellStyle3->WrapMode = System::Windows::Forms::DataGridViewTriState::True;
			this->canldestickDataGridView->RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this->canldestickDataGridView->Size = System::Drawing::Size(502, 207);
			this->canldestickDataGridView->TabIndex = 5;
			// 
			// redisplayButton
			// 
			this->redisplayButton->Location = System::Drawing::Point(920, 560);
			this->redisplayButton->Name = L"redisplayButton";
			this->redisplayButton->Size = System::Drawing::Size(150, 75);
			this->redisplayButton->TabIndex = 6;
			this->redisplayButton->Text = L"Redisplay";
			this->redisplayButton->UseVisualStyleBackColor = true;
			this->redisplayButton->Click += gcnew System::EventHandler(this, &FormMain::button1_Click);
			// 
			// chartStockData
			// 
			chartArea1->AxisY->IsStartedFromZero = false;
			chartArea1->Name = L"ChartStockData";
			chartArea2->AlignWithChartArea = L"ChartStockData";
			chartArea2->Name = L"ChartVolume";
			this->chartStockData->ChartAreas->Add(chartArea1);
			this->chartStockData->ChartAreas->Add(chartArea2);
			this->chartStockData->Dock = System::Windows::Forms::DockStyle::Top;
			legend1->Name = L"Legend1";
			this->chartStockData->Legends->Add(legend1);
			this->chartStockData->Location = System::Drawing::Point(0, 0);
			this->chartStockData->Name = L"chartStockData";
			series1->BorderColor = System::Drawing::Color::Transparent;
			series1->ChartArea = L"ChartStockData";
			series1->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Candlestick;
			series1->Color = System::Drawing::Color::Black;
			series1->CustomProperties = L"PriceDownColor=Red, PriceUpColor=Lime";
			series1->IsXValueIndexed = true;
			series1->Legend = L"Legend1";
			series1->Name = L"OHLCV";
			series1->XValueMember = L"Date";
			series1->XValueType = System::Windows::Forms::DataVisualization::Charting::ChartValueType::DateTime;
			series1->YValueMembers = L"high,low,open,close";
			series1->YValuesPerPoint = 4;
			series2->ChartArea = L"ChartVolume";
			series2->IsXValueIndexed = true;
			series2->Legend = L"Legend1";
			series2->Name = L"Volume";
			series2->XValueMember = L"date";
			series2->XValueType = System::Windows::Forms::DataVisualization::Charting::ChartValueType::DateTime;
			series2->YValueMembers = L"volume";
			series2->YValuesPerPoint = 2;
			series2->YValueType = System::Windows::Forms::DataVisualization::Charting::ChartValueType::UInt64;
			series3->ChartArea = L"ChartStockData";
			series3->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Point;
			series3->IsXValueIndexed = true;
			series3->Legend = L"Legend1";
			series3->MarkerBorderWidth = 8;
			series3->MarkerColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(255)), static_cast<System::Int32>(static_cast<System::Byte>(128)),
				static_cast<System::Int32>(static_cast<System::Byte>(0)));
			series3->MarkerStyle = System::Windows::Forms::DataVisualization::Charting::MarkerStyle::Circle;
			series3->Name = L"Dots";
			series4->ChartArea = L"ChartStockData";
			series4->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Point;
			series4->IsXValueIndexed = true;
			series4->Legend = L"Legend1";
			series4->MarkerBorderWidth = 8;
			series4->MarkerColor = System::Drawing::Color::DeepSkyBlue;
			series4->MarkerStyle = System::Windows::Forms::DataVisualization::Charting::MarkerStyle::Circle;
			series4->Name = L"Valleys";
			this->chartStockData->Series->Add(series1);
			this->chartStockData->Series->Add(series2);
			this->chartStockData->Series->Add(series3);
			this->chartStockData->Series->Add(series4);
			this->chartStockData->Size = System::Drawing::Size(1284, 452);
			this->chartStockData->TabIndex = 7;
			this->chartStockData->Text = L"chart1";
			// 
			// FormMain
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(1284, 677);
			this->Controls->Add(this->chartStockData);
			this->Controls->Add(this->redisplayButton);
			this->Controls->Add(this->canldestickDataGridView);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->dateTimePickerEndDate);
			this->Controls->Add(this->dateTimePickerStartDate);
			this->Name = L"FormMain";
			this->Text = L"FormMain";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->canldestickDataGridView))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->chartStockData))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		// read candlestick data from a CSV file and display to DataGridView
		private: System::Void openFileDialog1_FileOk(System::Object^ sender, System::ComponentModel::CancelEventArgs^ e)
		{
			// get the selected file path
			String^ filePath = openFileDialogSelectFile->FileName;
			// read candlestick data from the CSV file
			candlestickList = candlestickUtilities::readCandlesticksFromCsv(filePath);
			// display the data in the DataGridView
			canldestickDataGridView->DataSource = candlestickList;

			// initialize start and end dates from the DateTimePickers
			DateTime startDate = dateTimePickerStartDate->Value;
			DateTime endDate = dateTimePickerEndDate->Value;

			// filter the candlestick data based on the selected date range
			System::Collections::Generic::List<aCandlestick^>^ filteredData = candlestickUtilities::filterCandlestickData(candlestickList, startDate, endDate);
			// convert the old filtered data to the new smart candlestick
			System::Collections::Generic::List<aSmartCandlestick^>^ smartFilteredData = candlestickUtilities::buildSmartCandlesticks(filteredData);
			// send the updated list to the DataGridView for display
			canldestickDataGridView->DataSource = smartFilteredData;

			// Set the X & Y values of the new dot series to show peaks and valleys. 
			chartStockData->Series["Dots"]->XValueMember = "date";
			chartStockData->Series["Dots"]->YValueMembers = "peakValue";

			// Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
			chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aSmartCandlestick^>(smartFilteredData);
			chartStockData->DataBind();

			// find the number of files the user selected
			int numFiles = openFileDialogSelectFile->FileNames->Length;

			// for every file the user selected, create a new form and display it
			for (int i = 0; i < numFiles; i++)
			{
				// instantiate a form of type form
				FormMain^ f = gcnew FormMain();
				// construct the form with the file path and the start and end dates
				f->FormMain::FormMain(openFileDialogSelectFile->FileNames[i], startDate, endDate);
				// modify the title at the top of the form to include the file name
				f->Text = "Candlestick Chart - " + Path::GetFileName(openFileDialogSelectFile->FileNames[i]);
				f->Show();
			}
		}

		// mistaken event handler for label click, not used in this application
		private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) 
		{

		}

		// filter the candlestick data based on the selected start and end dates and redisplay
		private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e)
		{
			// initialize start and end dates from the DateTimePickers
			DateTime startDate = dateTimePickerStartDate->Value;
			DateTime endDate = dateTimePickerEndDate->Value;

			// filter the candlestick data based on the selected date range
			System::Collections::Generic::List<aCandlestick^>^ filteredData = candlestickUtilities::filterCandlestickData(candlestickList, startDate, endDate);
			// convert the filtered data to smart candlesticks so the peaks and valleys are recalculated for the new range
			System::Collections::Generic::List<aSmartCandlestick^>^ smartFilteredData = candlestickUtilities::buildSmartCandlesticks(filteredData);
			// send the updated list to the DataGridView for display
			canldestickDataGridView->DataSource = smartFilteredData;

			// Set the X & Y values of the dot series to show peaks and valleys.
			chartStockData->Series["Dots"]->XValueMember = "date";
			chartStockData->Series["Dots"]->YValueMembers = "peakValue";
			// Set the X & Y values of the valley series.
			chartStockData->Series["Valleys"]->XValueMember = "date";
			chartStockData->Series["Valleys"]->YValueMembers = "valleyValue";

			// Bind the chart to the list of smart candlesticks (DataSource should be a list or table, not a filename)
			chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aSmartCandlestick^>(smartFilteredData);
			// Refresh the chart to reflect the new data
			chartStockData->DataBind();
		}

		// function definitions in cpp file
	};


}