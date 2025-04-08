import re
from collections import Counter


def split_file(file_path, chunk_size):
    with open(file_path, 'r') as file:
        chunk = []
        for line in file:
            chunk.append(line.strip())
            if len(chunk) >= chunk_size:
                yield chunk
                chunk = []
        if chunk:
            yield chunk


def extract_error_from_line(line):
    match = re.search(r"Error:\s*(\S+)",line)
    if match:
        return match.group(1)
    return None


def process_file(file_path, n, chunk_size):
    error_counts = Counter()

    for chunk in split_file(file_path, chunk_size):
        errors = [extract_error_from_line(line) for line in chunk]
        error_counts.update(error for error in errors if error)
    return error_counts.most_common(n)


file_path = "TextLogs.txt"
n = 5
chunk_size = 100000

top_errors = process_file(file_path, n, chunk_size)

for error, count in top_errors:
    print(f"{error}: {count}")



