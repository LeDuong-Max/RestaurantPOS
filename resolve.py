import os
import sys

def resolve_file(filepath):
    try:
        with open(filepath, 'r', encoding='utf-8-sig') as f:
            content = f.read()
            
        if '<<<<<<< HEAD' not in content:
            return
            
        # Clean up markers
        content = content.replace('<<<<<<< HEAD\n', '')
        content = content.replace('=======\n', '')
        content = content.replace('>>>>>>> origin/main\n', '')
        # Remove BOMs that might have been inline
        content = content.replace('\ufeff', '')
        
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print('Resolved ' + filepath)
    except Exception as e:
        print('Error resolving ' + filepath + ': ' + str(e))

for root, dirs, files in os.walk('.'):
    for file in files:
        if file.endswith('.cs'):
            resolve_file(os.path.join(root, file))
