ROOT="$(git rev-parse --show-toplevel)"

# use first argument as command if not a flag
if [[ "$1" != -* ]]; then
  export COMMAND=$1
fi

# read the argument values
# https://linuxhint.com/pass-named-argument-shell-script/
while [[ "$#" -gt 0 ]]; do
  case $1 in
    -e|--environment) AZURE_ENV="$2"; shift;;
    --silent) SILENT=true; shift;;
  esac
  shift;
done

define() {
  # use default value if necessary
  if [ -z "${!1}" ]; then
    export "${1}"="${2}"
  fi

  # export variable to GITHUB_ENV
  if [ -n "$GITHUB_ENV" ]; then
    echo "${1}=${!1}" >>"$GITHUB_ENV"
  fi
}

define DOMAIN "bingogo.xyz"
define PROJECT "omsl"
define REVISION "latest"
define BUILD "$(date +%y%m%d-%H%M)"