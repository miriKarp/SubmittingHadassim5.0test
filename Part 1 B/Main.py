import pandas as pd


# 1
# A
def preper_data(file_path):
    df = pd.read_csv(file_path)
    df.columns = df.columns.str.strip()
    df['timestamp'] = pd.to_datetime(df['timestamp'], errors='coerce', dayfirst=True)
    df['value'] = pd.to_numeric(df['value'], errors='coerce')
    df = check_invalid_data(df)
    df = check_duplicates(df)
    return df


def check_duplicates(df):
    df = df.drop_duplicates(subset=['timestamp', 'value'], keep='first')
    return df


def check_invalid_data(df):
    df = df[df['timestamp'].notna()]
    df = df[df['value'] >= 0]
    return df


# B
def calculate_average_per_hour(df):
    df['timestamp'] = df['timestamp'].dt.floor('h')
    average_per_hour = df.groupby('timestamp')['value'].mean().reset_index()
    average_per_hour.columns = ['timestamp', 'average_value']
    return average_per_hour


def save_averaged_data(df, output_file_path):
    df.to_csv(output_file_path)
    print(f"Data saved in file: {output_file_path}")


# 2
def process_data_in_chunks(file_path):
    df = preper_data(file_path)
    df['day'] = df['timestamp'].dt.date
    days = df['day'].unique()
    all_averages = []

    for day in days:
        daily_df = df[df['day'] == day]
        average_per_hour = calculate_average_per_hour(daily_df)
        all_averages.append(average_per_hour)
    final_averages = pd.concat(all_averages, ignore_index=True)
    final_averages = final_averages.sort_values(by='timestamp')
    save_averaged_data(final_averages, "final2_averaged_time_series.csv")


def process_data():
    input_file_path = "time_series.csv"
    process_data_in_chunks(input_file_path)


process_data()

# 3
#ניתן לצרף את הממוצעים לפי יום ושעה וכל שורה חדשה שנקראת להוסיף לממוצע הכללי
#בקוד הרגיל בעצם אני קודם מנתחת את כל הקובץ ואז מחלקת אותו
#אבל בעת קבלת הנתונים בSTREAM אין אפשרות לנתח את כולו ולחלק אותו
# כיון שהוא מגיע בזמן אמת או שהוא גדול מאד ואין אפשרות לטעון אותו בבת אחת
#ולכן אפשר להוסיף לעמודה את הסכום הכולל של הערכים ואת הכמות
#וכך בזמן אמת להוסיף נתון שהגיע ע"פ העמודה הזו ולעדכן את הממוצע
#

#
# # 4
# def preper_data(file_path):
#     if file_path.endswith(".csv"):
#         df = pd.read_csv(file_path)
#     elif file_path.endswith(".parquet"):
#         df = pd.read_parquet(file_path)
#     else:
#         raise ValueError("Unsupported file format. Use .csv or .parquet")
#     df['timestamp'] = pd.to_datetime(df['timestamp'], errors='coerce')
#     df['value'] = pd.to_numeric(df['value'], errors='coerce')
#      df = check_invalid_data(df)
#      df = check_duplicates(df)
#     return df
#
#
# def calculate_average_per_hour(df):
#     df['timestamp'] = df['timestamp'].dt.floor('h')
#     average_per_hour = df.groupby('timestamp')['value'].mean().reset_index()
#     average_per_hour.columns = ['timestamp', 'average_value']
#     return average_per_hour
#
#
# def save_averaged_data(df, output_path):
#     df.to_csv(output_path, index=False)
#     print(f"Data saved in file: {output_file_path}")
#
#
# def process_data_in_chunks(file_path, output_path):
#     df = preper_data(file_path)
#     df['day'] = df['timestamp'].dt.date
#     days = df['day'].unique()
#     all_averages = []
#     for day in days:
#         daily_df = df[df['day'] == day]
#         average_per_hour = calculate_average_per_hour(daily_df)
#         all_averages.append(average_per_hour)
#     final_averages = pd.concat(all_averages, ignore_index=True)
#     final_averages = final_averages.sort_values(by='timestamp')
#     save_averaged_data(final_averages, output_path)
#
#
# def clean_data():
#     input_file_path = "time_series.parquet"
#     output_file_path = "finish_time_series.csv"
#     process_data_in_chunks(input_file_path, output_file_path)
#
# זהו קובץ הנשמר על בסיס עמודות כך שזה חוסך מקום ארגון וכן מאיץ שאילתות פיתוח והוא בינארי
#יעיל לקבצים גדולים מאד שכן הוא דוחס את הנתונים בצורה משמעותית מאד כך שזה מקל על האחסון
#הוא פורמט קובץ פתוח כך שיש לו תמיכה רחבה
#
#
