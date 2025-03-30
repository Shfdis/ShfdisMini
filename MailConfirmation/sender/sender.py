import smtplib
from email.mime.text import MIMEText
import os
def send_email_smtp(to_email, body):
    """
    Direct SMTP connection similar to SSMTP behavior
    """
    # Your SSMTP credentials (same as in /etc/ssmtp/ssmtp.conf)
    smtp_server = 'smtp.mail.ru'
    smtp_port = 465  # Typically 587 for STARTTLS, 465 for SSL
    username = 'shfdisminimail@mail.ru'
    password = os.environ['MAIL_PASSWORD']
    from_email = 'shfdisminimail@mail.ru'
    
    # Create message
    msg = MIMEText(body)
    msg['Subject'] = "ShfdisMini confirmation code"
    msg['From'] = from_email
    msg['To'] = to_email
    
    try:
        with smtplib.SMTP_SSL(smtp_server, smtp_port) as server:
            server.login(username, password)
            server.sendmail(from_email, [to_email], msg.as_string())
    except Exception as e:
        print(f"Failed to send email: {e}")
