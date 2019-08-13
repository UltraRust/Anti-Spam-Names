# Anti-Spam-Names
Replaces domain extensions or specific spam words in player's name. Also renames fake admins.

Just specify 'Spam keyword blacklist' with words (or domain extensions) you want to avoid. These words will be replaced with 'Replace for spam' expression.

You can also control fake admin players. Use 'Admin name blacklist' to specify words to avoid for non-admin players. It will ignore real administrators of course.

<pre><code>
{  
  "Admin name blacklist": [
    "Administrator",
    "Admin"
  ],
  "Replace for admin": "justplayer",
  "Replace for spam": "spam",
  "Spam keyword blacklist": [
    ".money",
    ".ru",
    ".com",
    ".pl",
    ".gg",
    ".de",
    ".net"
  ],
  "Check admin names": true,
  "Check spam names": true
}
  </code></pre>
